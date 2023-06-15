using System.ComponentModel;

namespace Tera_Web.Entities
{
    public class ConsolidatedObj
    {

        [DisplayName("Producto")]
        public string productName { get; set; } = string.Empty;
        [DisplayName("Unidad")]
        public string productMeasurementUnit { get; set; } = string.Empty;
        [DisplayName("Categoria")]
        public string categoryName { get; set; } = string.Empty;
        [DisplayName("Costo")]
        public int productCost { get; set; } = 0;
        [DisplayName("Sede")]
        public string siteName { get; set; } = string.Empty;
        [DisplayName("Rol")]
        public string roleName { get; set; } = string.Empty;
        [DisplayName("Cantidad")]
        public int orderDetailsQuantity { get; set; } = 0;
        [DisplayName("Fecha")]
        public DateTime orderDate { get; set; } = DateTime.MinValue;
        public DateTime MinDate { get; set; } = DateTime.MinValue;
        public DateTime MaxDate { get; set; } = DateTime.MinValue;

    }
}
