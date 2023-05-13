using static Tera_API.Entities.UserObj;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Tera_API.Entities;
using System.Reflection.Metadata;
using System.Reflection;
using System.Security.Cryptography;
using Microsoft.Win32;

namespace Tera_API.Models
{
    public class UserModel
    {
        //The ListUser function returns a list of all users in the database.
        public List<UserObj> ListUser(IConfiguration stringConnection)
        {

            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var SqlQuery = connection.Query<UserObj>("SELECT * FROM Users");
                return SqlQuery.ToList();
            }
        }

        //In the following procedure GetUser, I query the database for a user by ID.
        public UserObj GetUser(IConfiguration stringConnection, int id)
        {

            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var SqlQuery = connection.Query<UserObj>("SELECT * FROM Users WHERE userId =" + id.ToString()).ToList();
                return SqlQuery[0];
            }
        }

        //The following Register function is used to add a new user to a database.
        public int Register(UserObj user, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                return connection.Execute("InsertUser",
                    new
                    {
                        user.userEmail,
                        user.userPassword
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        //In the following EditUser function, what we are going to do is wait for the frontend
        //to connect to this API, passing us the edited parameters
        public int EditUser(UserObj user, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                return connection.Execute("EditUser",
                new
                {
                    //We pass the userId as a parameter
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
        public int DeleteUser(int userId, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                UserObj userObj = new UserObj();
                userObj.userId = userId;

                return connection.Execute("DeleteProduct",
                    new
                    {
                        userObj.userId
                    }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
