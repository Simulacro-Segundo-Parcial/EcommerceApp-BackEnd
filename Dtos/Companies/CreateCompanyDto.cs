using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.DTOs.Companies
{
    public class CreateCompanyDto
    {
        [Required, MaxLength(150)]
        public string Name { get; set; } = null!;

        [Required, MaxLength(300)]
        public string Description { get; set; } = null!;

        [MaxLength(100)]
        public string? Industry { get; set; }

        [MaxLength(300)]
        public string? Address { get; set; }

        [MaxLength(5000)]
        public string? LogoUrl { get; set; }
    }
}