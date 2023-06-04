using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tera_Web.Entities
{
    public class UserObj
    {
        [DisplayName("ID")]
        public int userId { get; set; } = 0;

        [DisplayName("Cédula")]
        public string userGovId { get; set; } = string.Empty;

        [DisplayName("Nombre")]
        public string userName { get; set; } = string.Empty;

        [DisplayName("Primer Apellido")]
        public string userFirstSurname { get; set; } = string.Empty;

        [DisplayName("Segundo Apellido")]
        public string userSecondSurname { get; set; } = string.Empty;

        [DisplayName("Correo")]
        public string userEmail { get; set; } = string.Empty;
        [DisplayName("Contraseña")]
        
        public string userPassword { get; set; } = string.Empty;

        [DisplayName("Confirmar Contraseña")]
        public string confirmPassword { get; set; } = string.Empty;

        [DisplayName("Activo")]
        public bool userActive { get; set; } = true;

        [DisplayName("Rol")]
        public int userRoleId { get; set; } = 0;

        [DisplayName("Sede")]
        public int userSiteId { get; set; } = 0;

        [DisplayName("Rol")]
        public string roleName { get; set; } = string.Empty;

        [DisplayName("Sede")]
        public string siteName { get; set; } = string.Empty;
    }
}
