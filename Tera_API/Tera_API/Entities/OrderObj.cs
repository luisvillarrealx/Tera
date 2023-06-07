using System.ComponentModel;

namespace Tera_API.Entities
{
    public class OrderObj
    {
        //Order Table
        public int orderId { get; set; } = 0;
        public int orderUserId { get; set; } = 0;
        public DateTime orderDate { get; set; } = DateTime.Now;
        public int orderTotal { get; set; } = 0;


        //Product Table 
        public int productId { get; set; } = 0;
        public string productName { get; set; } = string.Empty;
        public int orderDetailsQuantity { get; set; } = 0;
        public int productCost { get; set; } = 0;
        public string productMeasurementUnit { get; set; } = string.Empty;


        //User Table
        public int userId { get; set; } = 0;

        //Props for inner joins
        public string FullName { get; set; } = string.Empty;

    }
}
