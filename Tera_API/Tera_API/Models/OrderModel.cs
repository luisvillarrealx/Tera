using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using Tera_API.Entities;

namespace Tera_API.Models
{
    public class OrderModel
    {
        /// <summary>
        /// Obtiene una lista de todos los usuarios en la base de datos.
        /// </summary>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>Una lista de objetos OrderObj que representan a los usuarios.</returns>
        public List<OrderObj> ListOrder(IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var sqlQuery = connection.Query<OrderObj>("GetOrdersWithUserNames");
                return sqlQuery.ToList();
            }
        }

        /// <summary>
        /// Obtiene un usuario de la base de datos por su ID.
        /// </summary>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <param name="id">El ID del usuario.</param>
        /// <returns>Un objeto OrderObj que representa al usuario encontrado.</returns>
        public OrderObj GetOrder(IConfiguration stringConnection, int id)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var sqlQuery = connection.Query<OrderObj>("SELECT * FROM [Order] WHERE orderId = @Id", new { Id = id }).ToList();
                return sqlQuery.FirstOrDefault();
            }
        }

        /// <summary>
        /// Registra un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="orderObj">El objeto OrderObj que contiene los datos del usuario a registrar.</param>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
        public int Register(List<OrderObj> orders, IConfiguration stringConnection)
        {
            
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    // Insertar en la tabla "Order" y obtener el orderId generado
                    var orderId = connection.ExecuteScalar<int>("DECLARE @OrderId INT; " +
                        "INSERT INTO [dbo].[Order] (orderUserId, orderDate, orderTotal) VALUES (@orderUserId, @orderDate, @orderTotal); " +
                        "SET @OrderId = SCOPE_IDENTITY(); " +
                        "SELECT @OrderId;",
                        new
                        {
                            orderUserId = orders.First().userId,
                            orderDate = DateTime.Now.Date,
                            orderTotal = orders.Sum(o => o.productCost * o.orderDetailsQuantity)
                        },
                        transaction: transaction);

                    // Insertar los detalles del pedido en la tabla "OrderDetails"
                    foreach (var order in orders)
                    {
                        connection.Execute("INSERT INTO [dbo].[OrderDetails] (orderId, productId, orderDetailsQuantity) VALUES (@orderId, @productId, @orderDetailsQuantity);",
                            new
                            {
                                orderId,
                                order.productId,
                                order.orderDetailsQuantity
                            },
                            transaction: transaction);
                    }

                    transaction.Commit();
                    return orderId;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }


        /// <summary>
        /// Actualiza los datos de un usuario existente en la base de datos.
        /// </summary>
        /// <param name="orderObj">El objeto OrderObj que contiene los datos actualizados del usuario.</param>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
        public int EditOrder(OrderObj orderObj, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                return connection.Execute("Editorder",
                new
                {
                    orderObj.orderId,
                    orderObj.orderUserId,
                    orderObj.orderDate,
                    orderObj.orderTotal
                }, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Elimina un usuario de la base de datos por su ID.
        /// </summary>
        /// <param name="OrderId">El ID del usuario a eliminar.</param>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
        public int DeleteOrder(int OrderId, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                // Crea un objeto ProductObj con el ID del Producto a eliminar.
                OrderObj orderObj = new OrderObj();
                orderObj.orderId = OrderId;

                // Ejecuta un procedimiento almacenado para eliminar un Producto de la base de datos.
                return connection.Execute("Deleteorder",
                new
                {
                    orderObj.orderId
                }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
