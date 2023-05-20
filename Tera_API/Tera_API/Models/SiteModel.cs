using Dapper;
using System.Data;
using System.Data.SqlClient;
using Tera_API.Entities;

namespace Tera_API.Models
{
    public class SiteModel
    {
        /// <summary>
        /// Obtiene una lista de todos los sitios en la base de datos.
        /// </summary>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>Una lista de objetos SiteObj que representan a los sitios.</returns>
        public List<SiteObj> ListSite(IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                // Ejecuta una consulta SQL para obtener todos los sitios ordenados por productId.
                var SqlQuery = connection.Query<SiteObj>("SELECT * FROM Sites ORDER BY siteId ASC");
                return SqlQuery.ToList();
            }
        }

        /// <summary>
        /// Obtiene un sitio de la base de datos por su ID.
        /// </summary>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <param name="id">El ID del sitio.</param>
        /// <returns>Un objeto SiteObj que representa al sitio encontrado.</returns>
        public SiteObj GetSite(IConfiguration stringConnection, int id)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                // Ejecuta una consulta SQL para obtener un sitio específico según el productId.
                var SqlQuery = connection.Query<SiteObj>("SELECT * FROM Sites WHERE siteId =" + id.ToString()).ToList();
                return SqlQuery[0];
            }
        }

        /// <summary>
        /// Registra un nuevo sitio en la base de datos.
        /// </summary>
        /// <param name="siteObj">El objeto SiteObj que contiene los datos del sitio a registrar.</param>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
        public int Register(SiteObj siteObj, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                // Ejecuta un procedimiento almacenado para insertar un nuevo sitio en la base de datos.
                return connection.Execute("InsertSite",
                    new
                    {
                        siteObj.siteName,
                        siteObj.siteUbication
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Actualiza los datos de un sitio existente en la base de datos.
        /// </summary>
        /// <param name="siteObj">El objeto SiteObj que contiene los datos actualizados del sitio.</param>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
        public int EditSite(SiteObj siteObj, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                // Ejecuta un procedimiento almacenado para editar un sitio en la base de datos.
                return connection.Execute("EditSite",
                    new
                    {
                        siteObj.siteId,
                        siteObj.siteName,
                        siteObj.siteUbication
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary
        /// Elimina un sitio de la base de datos por su ID./// </summary>
        /// <param name="SiteId">El ID del sitio a eliminar.</param>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
        public int DeleteSite(int SiteId, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                // Crea un objeto SiteObj con el ID del sitio a eliminar.
                SiteObj siteObj = new SiteObj();
                siteObj.siteId = SiteId;

                // Ejecuta un procedimiento almacenado para eliminar un sitio de la base de datos.
                return connection.Execute("DeleteSite",
                    new
                    {
                        siteObj.siteId
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        // Esto es para un combobox

        /// <summary>
        /// Consulta todos los roles de la base de datos para llenar un combobox.
        /// </summary>
        /// <param name="stringConnection">La cadena de conexión a la base de datos.</param>
        /// <returns>Una lista de objetos RoleObj que representan los roles.</returns>
        public List<SiteObj> ComboBoxSites(IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var datos = connection.Query<SiteObj>("SELECT * FROM Sites").ToList();
                return datos;
            }
        }
    }
}