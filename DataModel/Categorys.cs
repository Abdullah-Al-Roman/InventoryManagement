using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.DataModel
{
    public class Categorys
    {
        [Key]
        public Guid CategoryID { get; set; }
        [RegularExpression(@"^[a-zA-Z\s]+$")]
        public string? CategoryName { get; set; }
    }
}
