﻿using static Tera_API.Entities.UserObj;
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
                var SqlQuery = connection.Query<UserObj>("SELECT * from Users");
                return SqlQuery.ToList();
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
                        user.email,
                        user.password
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
                        user.email,
                        user.password,
                        user.active,
                        user.idRole

                    }, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
