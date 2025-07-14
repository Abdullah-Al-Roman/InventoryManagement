using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.DataModel
{
    public class Clients
    {
        [Key]
        public Guid ClientID { get; set; }
        public string? ClientName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

    }
}
