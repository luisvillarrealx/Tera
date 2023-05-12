using System.Data.SqlClient;
using System.Data;
using Tera_API.Entities;
using Dapper;

namespace Tera_API.Models
{
    public class RoleModel
    {
        public List<RoleObj> LisRole(IConfiguration stringConnection)
        {

            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var SqlQuery = connection.Query<RoleObj>("SELECT * FROM Roles");
                return SqlQuery.ToList();
            }
        }
        public RoleObj GetRole(IConfiguration stringConnection, int id)
        {

            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var SqlQuery = connection.Query<RoleObj>("SELECT * FROM Roles WHERE roleId =" + id.ToString()).ToList();
                return SqlQuery[0];
            }
        }
        //esto es para un combobox
        public List<RoleObj> ConsultarRoles(IConfiguration stringConnection)
        {

            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var datos = connection.Query<RoleObj>("SELECT * FROM Roles").ToList();
                return datos;
            }
        }

        public int RegisterRole(RoleObj Role, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                return connection.Execute("InsertRole",
                    new
                    {
                        Role.roleName,
                        Role.roleDescription
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        public int EditRole(RoleObj Role, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                return connection.Execute("EditRole",
                    new
                    {
                        Role.roleId,
                        Role.roleName,
                        Role.roleDescription
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        public int DeleteRole(int RoleId, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                RoleObj ROL = new RoleObj();
                ROL.roleId = RoleId;
                return connection.Execute("DeleteRole",
                    new
                    {
                        ROL.roleId
                    }, commandType: CommandType.StoredProcedure);
            }
        }



    }
}
