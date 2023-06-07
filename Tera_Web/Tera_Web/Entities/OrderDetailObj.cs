using System.ComponentModel;

namespace Tera_Web.Entities
{
    public class OrderDetailObj
    {
        //OrderDetails Table
        [DisplayName("ID")]
        public int orderDetailsId { get; set; } = 0;
        [DisplayName("Id de orden")]
        public int orderId { get; set; } = 0;
        [DisplayName("Id producto")]
        public int productId { get; set; } = 0;
        [DisplayName("Cantidad")]
        public int orderDetailsQuantity { get; set; } = 0;

        //Product Table
        [DisplayName("Nombre")]
        public string productName { get; set; } = string.Empty;
    }
}
