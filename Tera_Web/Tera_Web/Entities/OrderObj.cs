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



        [DisplayName("ID")]
        public int productId { get; set; } = 0;


        [DisplayName("Producto")]
        public string productName { get; set; } = string.Empty;

        [DisplayName("Cantidad")]
        public int cuantity { get; set; } = 0;
        [DisplayName("Precio (₡)")]
        public int productCost { get; set; } = 0;

        [DisplayName("Unidad")]
        public string productMeasurementUnit { get; set; } = string.Empty;
    }
}
