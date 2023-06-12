using System.ComponentModel;

namespace Tera_Web.Entities
{
    public class CategoryObj
    {
        [DisplayName("ID")]
        public int categoryId { get; set; } = 0;
        [DisplayName("Nombre")]
        public string CategoryName { get; set; } = string.Empty;
    }
}
