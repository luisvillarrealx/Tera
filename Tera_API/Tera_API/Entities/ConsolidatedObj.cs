using System.ComponentModel;

namespace Tera_Web.Entities
{
    public class ConsolidatedObj
    {
        [DisplayName("ID")]
        public int orderId { get; set; } = 0;
        [DisplayName("Usuario")]
        public string orderUser { get; set; } = string.Empty;
        [DisplayName("Fecha")]
        public DateTime orderDate { get; set; } = DateTime.Now;
        [DisplayName("Total")]
        public int orderTotal { get; set; } = 0;
        [DisplayName("ID")]
        public int orderDetailsId { get; set; } = 0;
        [DisplayName("Id de orden")]
        public int productId { get; set; } = 0;
        [DisplayName("Producto")]
        public string productName { get; set; } = string.Empty;
        [DisplayName("Detalle")]
        public int orderDetailsQuantity { get; set; } = 0;

    }
}
