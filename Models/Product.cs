using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApi.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [Range(0.0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public ProductStatus Status { get; set; } = ProductStatus.Draft;
        
        [MaxLength(5000)]
        public string? ImageUrl { get; set; }

        // Relaciones
        [Required]
        public int CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public Company Company { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PublishedAt { get; set; }
    }
}