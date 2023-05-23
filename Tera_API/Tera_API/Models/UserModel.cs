using Dapper;
using System.Data;
using System.Data.SqlClient;
using Tera_API.Entities;

namespace Tera_API.Models
{
    public class UserModel
    {
        /// Obtiene una lista de todos los usuarios en la base de datos.
        public List<UserObj> ListUser(IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var sqlQuery = connection.Query<UserObj>("GetUsersWithRoleAndSiteNames");
                return sqlQuery.ToList();
            }
        }

        /// Obtiene un usuario de la base de datos por su ID.
        public UserObj GetUser(IConfiguration stringConnection, int id)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var sqlQuery = connection.Query<UserObj>("SELECT * FROM Users WHERE userId = @Id", new { Id = id }).ToList();
                return sqlQuery.FirstOrDefault();
            }
        }

        /// Registra un nuevo usuario en la base de datos.
        public int Register(UserObj user, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                return connection.Execute("InsertUser",
                    new
                    {
                        user.userEmail,
                        user.userPassword,
                        user.userRoleId,
                        user.userSiteId
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        public int ChangeUserActive(UserObj user, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                return connection.Execute("ChangeUserActive",
                new
                {
                    user.userId,
                    user.userGovId,
                    user.userName,
                    user.userFirstSurname,
                    user.userSecondSurname,
                    user.userEmail,
                    user.userPassword,
                    user.userActive,
                    user.userRoleId,
                    user.userSiteId
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public string EmailExists(string validateEmailExists)
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:7021//api/EmailExists?validateEmailExists=" + validateEmailExists;
                HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadFromJsonAsync<string>().Result;

                return "ERROR";
            }
        }

        /// Actualiza los datos de un usuario existente en la base de datos.
        public int EditUser(UserObj user, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                return connection.Execute("EditUser",
                new
                {
                    user.userId,
                    user.userGovId,
                    user.userName,
                    user.userFirstSurname,
                    user.userSecondSurname,
                    user.userEmail,
                    user.userPassword,
                    user.userActive,
                    user.userRoleId,
                    user.userSiteId
                }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}