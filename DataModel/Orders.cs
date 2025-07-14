using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.DataModel
{
    public class Orders
    {
        [Key]
        public Guid OrderId { get; set; }
        public float? Subtotal { get; set; }
        public float Discount { get; set; }
        public float? GrandTotal { get; set; }
        public float Pay { get; set; }
        public float Due { get; set; }      
        public string? PaymentType { get; set; }
    }
}
