using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tera_Web.Entities
{
    public class UserEPObj
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
    }
}
