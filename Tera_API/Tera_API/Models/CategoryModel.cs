using System.Data.SqlClient;
using System.Data;
using Tera_API.Entities;
using Dapper;

namespace Tera_API.Models
{
    public class CategoryModel
    {
        /// <summary>
        /// Obtiene una lista de todos los roles en la base de datos.
        /// </summary>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>Una lista de objetos CategoryObj que representan a los roles.</returns>
        public List<CategoryObj> ListCategory(IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var SqlQuery = connection.Query<CategoryObj>("SELECT * FROM Categories ORDER BY categoryId ASC");
                return SqlQuery.ToList();
            }
        }

        /// <summary>
        /// Obtiene un rol de la base de datos por su ID.
        /// </summary>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <param name="id">El ID del rol.</param>
        /// <returns>Un objeto CategoryObj que representa al rol encontrado.</returns>
        public CategoryObj GetCategory(IConfiguration stringConnection, int id)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var SqlQuery = connection.Query<CategoryObj>("SELECT * FROM Roles WHERE roleId =" + id.ToString()).ToList();
                return SqlQuery[0];
            }
        }

        /// <summary>
        /// Registra un nuevo rol en la base de datos.
        /// </summary>
        /// <param name="Role">El objeto CategoryObj que contiene los datos del rol a registrar.</param>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
        public int RegisterCategory(CategoryObj Category, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                return connection.Execute("InsertCategory",
                    new
                    {
                        Category.CategoryName
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Actualiza los datos de un rol existente en la base de datos.
        /// </summary>
        /// <param name="Role">El objeto CategoryObj que contiene los datos actualizados del rol.</param>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
        public int EditCategory(CategoryObj Category, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                return connection.Execute("EditCategory", new
                {
                    Category.categoryId,
                    Category.CategoryName
                }, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Elimina un rol de la base de datos por su ID.
        /// </summary>
        /// <param name="RoleId">El ID del rol a eliminar.</param>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
        public int DeleteCategory(int categoryId, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                // Crea un objeto CategoryObj con el ID del Rol a eliminar.
                CategoryObj CategoryObj = new CategoryObj();
                CategoryObj.categoryId = categoryId;

                // Ejecuta un procedimiento almacenado para eliminar un Rol de la base de datos.
                return connection.Execute("DeleteCategory",
                    new
                    {
                        CategoryObj.categoryId
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        // Esto es para un combobox

        /// <summary>
        /// Consulta todos los roles de la base de datos para llenar un combobox.
        /// </summary>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>Una lista de objetos CategoryObj que representan los roles.</returns>
        public List<CategoryObj> ComboBoxCategory(IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var datos = connection.Query<CategoryObj>("SELECT * FROM Categories").ToList();
                return datos;
            }
        }
    }
}
