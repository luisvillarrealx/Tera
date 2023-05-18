using System.ComponentModel;

namespace Tera_Web.Entities
{
    public class OrderDetailObj
    {
        [DisplayName("ID")]
        public int orderDetailsId { get; set; } = 0;
        [DisplayName("Id de orden")]
        public int orderId { get; set; } = 0;
        [DisplayName("Id producto")]
        public int productId { get; set; } = 0;
        [DisplayName("Detalle")]
        public int orderDetailsQuantity { get; set; } = 0;
    }
}
