using System.ComponentModel;

namespace Tera_Web.Entities
{
    public class ProductObj
    {
        [DisplayName("ID")]
        public int productId { get; set; } = 0;

        [DisplayName("Producto")]
        public string productName { get; set; } = string.Empty;

        [DisplayName("Precio (₡)")]
        public int productCost { get; set; } = 0;

        [DisplayName("Unidad")]
        public string productMeasurementUnit { get; set; } = string.Empty;



        [DisplayName("Cantidad")]
        public int cuantity { get; set; } = 0;

        //CategoryObj
        [DisplayName("ID")]
        public int categoryId { get; set; } = 0;
        [DisplayName("Categoria")]
        public string categoryName { get; set; } = string.Empty;
    }
}
