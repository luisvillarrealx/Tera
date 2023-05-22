using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tera_Web.Entities
{
    public class UserRegisterObj
    {
        [DisplayName("ID")]
        public int userId { get; set; } = 0;

        [DisplayName("Correo")]
        [Required(ErrorMessage = "Por favor, ingresa un correo electrónico.")]
        [EmailAddress(ErrorMessage = "Por favor, ingresa un correo electrónico válido.")]
        public string userEmail { get; set; } = string.Empty;

        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "Por favor, ingresa una contraseña.")]
        public string userPassword { get; set; } = string.Empty;

        [DisplayName("Rol")]
        [Required(ErrorMessage = "Por favor, selecciona un rol.")]
        public int userRoleId { get; set; } = 0;

        [DisplayName("Sede")]
        [Required(ErrorMessage = "Por favor, selecciona una sede.")]
        public int userSiteId { get; set; } = 0;
    }
}
