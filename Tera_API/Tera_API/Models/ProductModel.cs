using System.Data.SqlClient;
using System.Data;
using System.Net.Http.Headers;
using Tera_API.Entities;
using Dapper;

namespace Tera_API.Models
{
    public class ProductModel
    {
        public List<ProductObj> ListProduct(IConfiguration stringConnection)
        {

            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var SqlQuery = connection.Query<ProductObj>("SELECT * FROM Products ORDER BY productId ASC");
                return SqlQuery.ToList();
            }
        }
        public ProductObj GetProduct(IConfiguration stringConnection, int id)
        {

            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var SqlQuery = connection.Query<ProductObj>("SELECT * FROM Products WHERE productId =" + id.ToString()).ToList();
                return SqlQuery[0];
            }
        }

        public int Register(ProductObj ProductObj, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                return connection.Execute("InsertProduct",
                    new
                    {
                        ProductObj.productName,
                        ProductObj.productCost,
                        ProductObj.productMeasurementUnit
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        public int EditProduct(ProductObj ProductObj, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {

                return connection.Execute("EditProduct",
                    new
                    {
                        ProductObj.productId,
                        ProductObj.productName,
                        ProductObj.productCost,
                        ProductObj.productMeasurementUnit
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        public int DeleteProduct(int ProductId, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                ProductObj productObj = new ProductObj();
                productObj.productId = ProductId;

                return connection.Execute("DeleteProduct",
                    new
                    {
                        productObj.productId
                    }, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
