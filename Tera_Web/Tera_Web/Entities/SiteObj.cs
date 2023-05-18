using System.ComponentModel;

namespace Tera_Web.Entities
{
    public class SiteObj
    {
        [DisplayName("ID")]
        public int siteId { get; set; } = 0;
        [DisplayName("Nombre")]
        public string siteName { get; set; } = string.Empty;
        [DisplayName("Ubicación")]
        public string siteUbication { get; set; } = string.Empty;
    }
}
