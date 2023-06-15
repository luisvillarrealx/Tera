using System.ComponentModel;

namespace Tera_Web.Entities
{
    public class ConsolidatedObj
    {
        public string productName { get; set; } = string.Empty;
        public string productMeasurementUnit { get; set; } = string.Empty;
        public string categoryName { get; set; } = string.Empty;
        public int productCost { get; set; } = 0;
        public string siteName { get; set; } = string.Empty;
        public string roleName { get; set; } = string.Empty;
        public int orderDetailsQuantity { get; set; } = 0;
        public DateTime orderDate { get; set; } = DateTime.MinValue;
        public DateTime MinDate { get; set; } = DateTime.MinValue;
        public DateTime MaxDate { get; set; } = DateTime.MinValue;
    }
}
