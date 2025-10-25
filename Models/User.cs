using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EcommerceApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, EmailAddress]
        [MaxLength(500)]
        public string Email { get; set; } = null!;

        [Required]
        [JsonIgnore]
        [MaxLength(500)]
        public string PasswordHash { get; set; } = null!;

        [Required]
        public UserRole Role { get; set; } = UserRole.Customer;

        [Required, MaxLength(100)]
        public string FullName { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        // Relaciones
        public int? CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public Company? Company { get; set; }

        [JsonIgnore]
        public List<CartItem> CartItems { get; set; } = [];

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}