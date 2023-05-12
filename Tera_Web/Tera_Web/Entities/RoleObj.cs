using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;

namespace Tera_Web.Entities
{
    public class RoleObj
    {
        [DisplayName("Id")]
        public int roleId { get; set; } = 0;
        [DisplayName("Rol")]
        public string roleName { get; set; } = string.Empty;
        [DisplayName("Descripción")]
        public string roleDescription { get; set; } = string.Empty;

    }
}
