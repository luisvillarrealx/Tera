using Dapper;
using System.Data;
using System.Data.SqlClient;
using Tera_API.Entities;

namespace Tera_API.Models
{
    public class UserModel
    {
        /// <summary>
        /// Obtiene una lista de todos los usuarios en la base de datos.
        /// </summary>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>Una lista de objetos UserObj que representan a los usuarios.</returns>
        public List<UserObj> ListUser(IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var sqlQuery = connection.Query<UserObj>("GetUsersWithRoleAndSiteNames");
                return sqlQuery.ToList();
            }
        }

        /// <summary>
        /// Obtiene un usuario de la base de datos por su ID.
        /// </summary>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <param name="id">El ID del usuario.</param>
        /// <returns>Un objeto UserObj que representa al usuario encontrado.</returns>
        public UserObj GetUser(IConfiguration stringConnection, int id)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var sqlQuery = connection.Query<UserObj>("SELECT * FROM Users WHERE userId = @Id", new { Id = id }).ToList();
                return sqlQuery.FirstOrDefault();
            }
        }

        /// <summary>
        /// Registra un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="user">El objeto UserObj que contiene los datos del usuario a registrar.</param>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
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

        /// <summary>
        /// Actualiza los datos de un usuario existente en la base de datos.
        /// </summary>
        /// <param name="user">El objeto UserObj que contiene los datos actualizados del usuario.</param>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
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

        /// <summary>
        /// Elimina un usuario de la base de datos por su ID.
        /// </summary>
        /// <param name="UserId">El ID del usuario a eliminar.</param>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
        //public int DeleteUser(int UserId, IConfiguration stringConnection)
        //{
        //    using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
        //    {
        //        // Crea un objeto UserObj con el ID del Usuario a eliminar.
        //        UserObj userObj = new UserObj();
        //        userObj.userId = UserId;
        //        // Ejecuta un procedimiento almacenado para eliminar un Sitio de la base de datos.
        //        return connection.Execute("DeleteUser",
        //        new
        //        {
        //            userObj.userId
        //        }, commandType: CommandType.StoredProcedure);
        //    }
        //}
    }
}