using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tera_Web.Entities
{
    public class UserObj
    {
        [DisplayName("Id")]
        public int userId { get; set; } = 0;
        [DisplayName("Cédula")]
        public int userGovId { get; set; } = 0;
        [DisplayName("Nombre")]
        public string userName { get; set; } = string.Empty;
        [DisplayName("Primer Apellido")]
        public string userFirstSurname { get; set; } = string.Empty;
        [DisplayName("Segundo Apellido")]
        public string userSecondSurname { get; set; } = string.Empty;
        [DisplayName("Correo")]
        public string email { get; set; } = string.Empty;
        [DisplayName("Contraseña")]
        public string password { get; set; } = string.Empty;
        [DisplayName("Activo")]
        public bool active { get; set; } = true;
        [DisplayName("Rol")]
        public int idRole { get; set; } = 0;
    }
}
