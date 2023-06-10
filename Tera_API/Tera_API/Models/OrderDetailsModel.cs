using Dapper;
using System.Data;
using System.Data.SqlClient;
using Tera_API.Entities;

namespace Tera_API.Models
{
    public class OrderDetailsModel
    {
        // Obtiene una lista de todos los detalles de los pedidos en la base de datos.
        public List<OrderDetailsObj> ListOrderOrderDetails(IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                // Crea una conexión a la base de datos utilizando la cadena de conexión proporcionada.
                // SqlConnection es una clase de .NET para conectarse a una base de datos SQL Server.
                // La cadena de conexión se obtiene de la configuración de la aplicación.
                // Aquí se utiliza la sección "ConnectionStrings:Connection" de la configuración.

                // Ejecuta una consulta SQL para obtener todos los detalles de los pedidos ordenados por orderId.
                // La consulta se realiza utilizando Dapper, una biblioteca que simplifica el acceso a la base de datos.
                // Se especifica el tipo de objeto que se espera como resultado (OrderDetailsObj) y la consulta SQL.
                var sqlQuery = connection.Query<OrderDetailsObj>("GetOrderDetailsWithProductNames");

                // Convierte los resultados de la consulta en una lista y la devuelve.
                return sqlQuery.ToList();
            }
        }
        
        // Obtiene una lista de todos los detalles de los pedidos en la base de datos.
        public List<OrderDetailsObj> GetOrderDetailsListUser(IConfiguration stringConnection , int orderId)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                // Crea una conexión a la base de datos utilizando la cadena de conexión proporcionada.
                // SqlConnection es una clase de .NET para conectarse a una base de datos SQL Server.
                // La cadena de conexión se obtiene de la configuración de la aplicación.
                // Aquí se utiliza la sección "ConnectionStrings:Connection" de la configuración.

                // Ejecuta una consulta SQL para obtener todos los detalles de los pedidos ordenados por orderId.
                // La consulta se realiza utilizando Dapper, una biblioteca que simplifica el acceso a la base de datos.
                // Se especifica el tipo de objeto que se espera como resultado (OrderDetailsObj) y la consulta SQL.
                var sqlQuery = connection.Query<OrderDetailsObj>("SELECT OD.[orderDetailsId]," +
                    " OD.[orderId]," +
                    " P.[productName]," +
                    " OD.[orderDetailsQuantity]\r\n    " +
                    "FROM [dbo].[OrderDetails] " +
                    "AS OD\r\n    " +
                    "INNER JOIN " +
                    "[dbo].[Products] " +
                    "AS P ON " +
                    "OD.[productId] = P.[productId]\r\n\t" +
                    "Where OD.orderId = @orderId", new { orderId });

                // Convierte los resultados de la consulta en una lista y la devuelve.
                return sqlQuery.ToList();
            }
        }

        // Obtiene un detalle de pedido de la base de datos por su ID.
        public OrderDetailsObj GetOrderDetails(IConfiguration stringConnection, int id)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                // Crea una conexión a la base de datos utilizando la cadena de conexión proporcionada.

                // Ejecuta una consulta SQL para obtener un detalle de pedido específico por su orderDetailsId.
                // En este caso, se utiliza un parámetro llamado "Id" en la consulta y se asigna el valor del parámetro "id".
                // Esto ayuda a prevenir ataques de inyección SQL al utilizar consultas parametrizadas.
                var sqlQuery = connection.Query<OrderDetailsObj>("SELECT * FROM OrderDetails WHERE orderDetailsId = @Id", new { Id = id }).ToList();

                // Devuelve el primer resultado de la lista o null si no hay resultados.
                return sqlQuery.FirstOrDefault();
            }
        }

        // Registra un nuevo detalle de pedido en la base de datos.
        public int Register(OrderDetailsObj orderDetailsObj, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                // Crea una conexión a la base de datos utilizando la cadena de conexión proporcionada.

                // Ejecuta un procedimiento almacenado para insertar un nuevo detalle de pedido en la base de datos.
                // Se especifica el nombre del procedimiento almacenado ("InsertOrderDetailsObj") y los parámetros necesarios.
                // Los valores de los parámetros se obtienen del objeto orderDetailsObj.
                // La función Execute devuelve el número de filas afectadas por la operación.
                return connection.Execute("InsertOrderDetails",
                    new
                    {
                        orderDetailsObj.orderId,
                        orderDetailsObj.productId,
                        orderDetailsObj.orderDetailsQuantity
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        // Actualiza los datos de un detalle de pedido existente en la base de datos.
        public int EditOrderDetails(OrderDetailsObj orderDetailsObj, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                // Crea una conexión a la base de datos utilizando la cadena de conexión proporcionada.
                // Ejecuta un procedimiento almacenado para editar un detalle de pedido en la base de datos.
                // Se especifica el nombre del procedimiento almacenado ("EditOrderDetailsObj") y los parámetros necesarios.
                // Los valores de los parámetros se obtienen del objeto orderDetailsObj.
                // La función Execute devuelve el número de filas afectadas por la operación.
                return connection.Execute("EditOrderDetails",
                new
                {
                    orderDetailsObj.orderDetailsId,
                    orderDetailsObj.orderId,
                    orderDetailsObj.productId,
                    orderDetailsObj.orderDetailsQuantity
                }, commandType: CommandType.StoredProcedure);
            }
        }

        // Elimina un detalle de pedido de la base de datos por su ID.
        public int DeleteOrderDetails(int OrderDetailsId, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                // Crea una conexión a la base de datos utilizando la cadena de conexión proporcionada.

                // Crea un objeto OrderDetailsObj con el ID del detalle de pedido a eliminar.
                // Esto se hace para poder pasar el ID como parámetro al procedimiento almacenado.
                OrderDetailsObj orderDetailsObj = new OrderDetailsObj();
                orderDetailsObj.orderDetailsId = OrderDetailsId;

                // Ejecuta un procedimiento almacenado para eliminar un detalle de pedido de la base de datos.
                // Se especifica el nombre del procedimiento almacenado ("DeleteOrderDetailsObj") y los parámetros necesarios.
                // Los valores de los parámetros se obtienen del objeto orderDetailsObj.
                // La función Execute devuelve el número de filas afectadas por la operación.
                return connection.Execute("DeleteOrderDetails",
                new
                {
                    orderDetailsObj.orderDetailsId
                }, commandType: CommandType.StoredProcedure);
            }
        }
        public List<ProductObj> ComboBoxProduct(IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var datos = connection.Query<ProductObj>("SELECT * FROM Products").ToList();
                return datos;
            }
        }
    }
}
