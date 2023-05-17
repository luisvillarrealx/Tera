namespace Tera_API.Entities
{
    public class OrderObj
    {
        public int orderId { get; set; } = 0;
        public string orderUser { get; set; } = string.Empty;
        public DateTime orderDate { get; set; } = DateTime.Now;
        public int orderTotal { get; set; } = 0;
    }
}
