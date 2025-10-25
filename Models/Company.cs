using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceApi.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = null!;

        [Required, MaxLength(300)]
        public string Description { get; set; } = null!;

        [MaxLength(100)]
        public string? Industry { get; set; }

        public CompanyStatus Status { get; set; } = CompanyStatus.PendingVerification;

        [MaxLength(300)]
        public string? Address { get; set; }
        
        [MaxLength(5000)]
        public string? LogoUrl { get; set; }

        [JsonIgnore]
        public List<User> Users { get; set; } = [];

        [JsonIgnore]
        public List<Product> Products { get; set; } = [];

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}