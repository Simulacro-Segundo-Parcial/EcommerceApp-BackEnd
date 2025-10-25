using System.ComponentModel.DataAnnotations;
using EcommerceApi.Models;

namespace EcommerceApi.DTOs.Products
{
    public class CreateProductDto
    {
        [Required, MaxLength(150)]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required, Range(0.0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [MaxLength(5000)]
        public string? ImageUrl { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
}