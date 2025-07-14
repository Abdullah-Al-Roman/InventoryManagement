namespace InventoryManagement.Models
{
    public class LowStockProductVM
    {
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public string? VendorName { get; set; }
    }
}
