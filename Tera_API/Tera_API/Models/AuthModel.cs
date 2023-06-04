using Dapper;
using System.Data;
using System.Data.SqlClient;
using Tera_API.Entities;

namespace Tera_API.Models
{
    public class AuthModel
    {
        public UserObj? UserValidate(UserObj userObj, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var data = connection.Query<UserObj>("ValidateCredentials",
                    new
                    {
                        userObj.userEmail,
                        userObj.userPassword
                    },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();

                if (data != null)
                {
                    return data;
                }
                return null;
            }
        }
    }
}
