using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tera_Web.Entities
{
    public class UserLoginObj
    {
        [DisplayName("ID")]
        public int userId { get; set; } = 0;
        public string userGovId { get; set; } = string.Empty;
        public string userName { get; set; } = string.Empty;
        public string userFirstSurname { get; set; } = string.Empty;
        public string userSecondSurname { get; set; } = string.Empty;

        [DisplayName("Correo")]
        [Required(ErrorMessage = "Por favor, ingresa un correo electrónico.")]
        [EmailAddress(ErrorMessage = "Por favor, ingresa un correo electrónico válido.")]
        public string userEmail { get; set; } = string.Empty;

        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "Por favor, ingresa una contraseña.")]
        public string userPassword { get; set; } = string.Empty;
        public bool userActive { get; set; } = false;
        public int userRoleId { get; set; } = 0;
        public int userSiteId { get; set; } = 0;

    }
}
