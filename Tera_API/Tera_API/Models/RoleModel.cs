using Dapper;
using System.Data;
using System.Data.SqlClient;
using Tera_API.Entities;

namespace Tera_API.Models
{
    public class RoleModel
    {
        /// <summary>
        /// Obtiene una lista de todos los roles en la base de datos.
        /// </summary>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>Una lista de objetos RoleObj que representan a los roles.</returns>
        public List<RoleObj> LisRole(IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var SqlQuery = connection.Query<RoleObj>("SELECT * FROM Roles ORDER BY roleId ASC");
                return SqlQuery.ToList();
            }
        }

        /// <summary>
        /// Obtiene un rol de la base de datos por su ID.
        /// </summary>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <param name="id">El ID del rol.</param>
        /// <returns>Un objeto RoleObj que representa al rol encontrado.</returns>
        public RoleObj GetRole(IConfiguration stringConnection, int id)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var SqlQuery = connection.Query<RoleObj>("SELECT * FROM Roles WHERE roleId =" + id.ToString()).ToList();
                return SqlQuery[0];
            }
        }

        /// <summary>
        /// Registra un nuevo rol en la base de datos.
        /// </summary>
        /// <param name="Role">El objeto RoleObj que contiene los datos del rol a registrar.</param>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
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

        /// <summary>
        /// Actualiza los datos de un rol existente en la base de datos.
        /// </summary>
        /// <param name="Role">El objeto RoleObj que contiene los datos actualizados del rol.</param>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
        public int EditRole(RoleObj Role, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                return connection.Execute("EditRole", new
                {
                    Role.roleId,
                    Role.roleName,
                    Role.roleDescription
                }, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Elimina un rol de la base de datos por su ID.
        /// </summary>
        /// <param name="RoleId">El ID del rol a eliminar.</param>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
        public int DeleteRole(int RoleId, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                // Crea un objeto RoleObj con el ID del Rol a eliminar.
                RoleObj roleObj = new RoleObj();
                roleObj.roleId = RoleId;

                // Ejecuta un procedimiento almacenado para eliminar un Rol de la base de datos.
                return connection.Execute("DeleteRole",
                    new
                    {
                        roleObj.roleId
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        // Esto es para un combobox

        /// <summary>
        /// Consulta todos los roles de la base de datos para llenar un combobox.
        /// </summary>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>Una lista de objetos RoleObj que representan los roles.</returns>
        public List<RoleObj> ComboBoxRoles(IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var datos = connection.Query<RoleObj>("SELECT * FROM Roles").ToList();
                return datos;
            }
        }

    }
}
