using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.DataModel
{
    public class Productdetails
    {
        [Key]
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public float Total { get; set; }

    }
}
