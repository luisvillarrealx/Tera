using System.ComponentModel;

namespace Tera_Web.Entities
{
    public class OrderObj
    {
        //Order Table
        [DisplayName("ID")]
        public int orderId { get; set; } = 0;
        [DisplayName("Usuario")]
        public int orderUserId { get; set; } = 0;
        [DisplayName("Fecha")]
        public DateTime orderDate { get; set; } = DateTime.Now;
        [DisplayName("Total")]
        public int orderTotal { get; set; } = 0;


        //Product Table 
        [DisplayName("ID")]
        public int productId { get; set; } = 0;

        [DisplayName("Producto")]
        public string productName { get; set; } = string.Empty;

        [DisplayName("Cantidad")]
        public int orderDetailsQuantity { get; set; } = 0;

        [DisplayName("Precio (₡)")]
        public int productCost { get; set; } = 0;

        [DisplayName("Unidad")]
        public string productMeasurementUnit { get; set; } = string.Empty;


        //User Table
        [DisplayName("ID")]
        public int userId { get; set; } = 0;

        //Props for inner joins
        [DisplayName("Nombre completo")]
        public string FullName { get; set; } = string.Empty;
    }
}
