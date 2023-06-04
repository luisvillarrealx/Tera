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
                var sqlQuery = connection.Query<ConsolidatedObj>("GetConsolidated");
                return sqlQuery.ToList();
            }
        }
    }
}
