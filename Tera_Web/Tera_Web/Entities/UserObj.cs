using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tera_Web.Entities
{
    public class UserObj
    {
        [DisplayName("Id")]
        public int userId { get; set; } = 0;

        [DisplayName("Cédula")]
        [Required(ErrorMessage = "Por favor, ingresa la cédula.")]
        [StringLength(9, ErrorMessage = "La cédula debe tener 9 dígitos.", MinimumLength = 9)]
        public string userGovId { get; set; } = string.Empty;

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Por favor, ingresa la cédula para que salga tu nombre")]
        public string userName { get; set; } = string.Empty;

        [DisplayName("Primer Apellido")]
        [Required(ErrorMessage = "Por favor, ingresa la cédula para que salga tu 1°Apellido")]
        public string userFirstSurname { get; set; } = string.Empty;

        [DisplayName("Segundo Apellido")]
        [Required(ErrorMessage = "Por favor, ingresa la cédula para que salga tu 2°Apellido")]
        public string userSecondSurname { get; set; } = string.Empty;

        [DisplayName("Correo")]
        [Required(ErrorMessage = "Por favor, ingresa un correo electrónico.")]
        [EmailAddress(ErrorMessage = "Por favor, ingresa un correo electrónico válido.")]
        public string userEmail { get; set; } = string.Empty;

        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "Por favor, ingresa una contraseña.")]
        public string userPassword { get; set; } = string.Empty;

        [DisplayName("Activo")]
        public bool userActive { get; set; } = true;

        [DisplayName("Rol")]
        public int userRoleId { get; set; } = 0;

        [DisplayName("Sede")]
        public int userSiteId { get; set; } = 0;
    }
}
