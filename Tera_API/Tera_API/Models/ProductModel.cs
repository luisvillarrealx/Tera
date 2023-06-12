using Dapper;
using System.Data;
using System.Data.SqlClient;
using Tera_API.Entities;

namespace Tera_API.Models
{
    public class ProductModel
    {
        /// <summary>
        /// Obtiene una lista de todos los productos en la base de datos.
        /// </summary>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>Una lista de objetos ProductObj que representan a los productos.</returns>
        public List<ProductObj> ListProduct(IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var SqlQuery = connection.Query<ProductObj>("GetProductsWithCategoryNames");
                return SqlQuery.ToList();
            }
        }

        /// <summary>
        /// Obtiene un producto de la base de datos por su ID.
        /// </summary>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <param name="id">El ID del producto.</param>
        /// <returns>Un objeto ProductObj que representa al producto encontrado.</returns>
        public ProductObj GetProduct(IConfiguration stringConnection, int id)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var SqlQuery = connection.Query<ProductObj>("SELECT * FROM Products WHERE productId =" + id.ToString()).ToList();
                return SqlQuery[0];
            }
        }

        /// <summary>
        /// Registra un nuevo producto en la base de datos.
        /// </summary>
        /// <param name="productObj">El objeto ProductObj que contiene los datos del producto a registrar.</param>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
        public int Register(ProductObj productObj, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                return connection.Execute("InsertProduct",
                    new
                    {
                        productObj.productName,
                        productObj.productCost,
                        productObj.productMeasurementUnit,
                        productObj.categoryId
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Actualiza los datos de un producto existente en la base de datos.
        /// </summary>
        /// <param name="productObj">El objeto ProductObj que contiene los datos actualizados del producto.</param>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
        public int EditProduct(ProductObj productObj, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                return connection.Execute("EditProduct",
                    new
                    {
                        productObj.productId,
                        productObj.productName,
                        productObj.productCost,
                        productObj.productMeasurementUnit,
                        productObj.categoryId
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Elimina un producto de la base de datos por su ID.
        /// </summary>
        /// <param name="productId">El ID del producto a eliminar.</param>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
        public int DeleteProduct(int productId, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                // Crea un objeto ProductObj con el ID del Producto a eliminar.
                ProductObj productObj = new ProductObj();
                productObj.productId = productId;

                // Ejecuta un procedimiento almacenado para eliminar un Producto de la base de datos.
                return connection.Execute("DeleteProduct",
                    new
                    {
                        productObj.productId
                    }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}