
namespace Tera_API.Entities
{
    public class ProductObj
    {
        public int productId { get; set; } = 0;

        public string productName { get; set; } = string.Empty;

        public float productCost { get; set; } = float.MinValue;

        public string productMeasurementUnit { get; set; } = string.Empty;

        //CategoryObj
        public int categoryId { get; set; } = 0;
        public string categoryName { get; set; } = string.Empty;

    }
}
