using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.DataModel
{
    public class Products
    {
        [Key]
        public Guid ProductID { get; set; }
        public Guid VendorID { get; set; }
        public string? ProductName { get; set; }
        public string? Category { get; set; }
        public int Quantity { get; set; }
        public string? Price { get; set; }
        public string? Description { get; set; }
    }
}
