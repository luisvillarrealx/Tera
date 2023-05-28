using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tera_Web.Entities
{
    public class UserRegisterObj
    {
        [DisplayName("ID")]
        public int userId { get; set; } = 0;

        [DisplayName("Correo")]
        [EmailAddress(ErrorMessage = "Por favor, ingresa un correo electrónico válido.")]
        public string userEmail { get; set; } = string.Empty;

        [DisplayName("Contraseña")]
        public string userPassword { get; set; } = string.Empty;

        [DisplayName("Confirmar Contraseña")]
        public string confirmPassword { get; set; } = string.Empty;

        [DisplayName("Rol")]
        public int userRoleId { get; set; } = 0;

        [DisplayName("Sede")]
        public int userSiteId { get; set; } = 0;
    }
}
