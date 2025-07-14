using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.DataModel
{
    public class Stocks
    {
        [Key]
        public Guid StockID { get; set; }
        public int Quantity { get; set; }
    }
}
