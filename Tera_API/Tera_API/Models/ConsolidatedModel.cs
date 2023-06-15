using Dapper;
using System.Data.SqlClient;
using Tera_Web.Entities;

namespace Tera_API.Models
{
    public class ConsolidatedModel
    {
        public List<ConsolidatedObj> ListConsolidated(IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var sqlQuery = connection.Query<ConsolidatedObj>("SelectRawData");
                return sqlQuery.ToList();
            }
        }
        public List<ConsolidatedObj> ListConsolidated(IConfiguration configuration, DateTime min, DateTime max)
        {
            string connectionString = configuration.GetConnectionString("Connection");

            using (var connection = new SqlConnection(connectionString))
            {
                var sqlQuery = connection.Query<ConsolidatedObj>("SelectRawDataFiltered");
                return sqlQuery.ToList();
            }
        }

    }
}
