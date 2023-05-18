using System.ComponentModel;

namespace Tera_Web.Entities
{
    public class OrderObj
    {
        [DisplayName("ID")]
        public int orderId { get; set; } = 0;
        [DisplayName("Usuario")]
        public string orderUser { get; set; } = string.Empty;
        [DisplayName("Fecha")]
        public DateTime orderDate { get; set; } = DateTime.Now;
        [DisplayName("Total")]
        public int orderTotal { get; set; } = 0;
    }
}
