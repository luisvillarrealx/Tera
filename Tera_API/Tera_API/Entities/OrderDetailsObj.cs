namespace Tera_API.Entities
{
    public class OrderDetailsObj
    {
        //OrderDetails Table
        public int orderDetailsId { get; set; } = 0;
        public int orderId { get; set; } = 0;
        public int productId { get; set; } = 0;
        public int orderDetailsQuantity { get; set; } = 0;

        //Product Table
        public string productName { get; set; } = string.Empty;
    }
}
