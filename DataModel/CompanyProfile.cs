using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.DataModel
{
    public class CompanyProfile
    {
        [Key]
        public int CompanyProfileId { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? LogoPath { get; set; }
        public string? Slogan { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
