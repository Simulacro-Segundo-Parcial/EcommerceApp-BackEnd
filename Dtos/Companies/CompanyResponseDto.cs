using EcommerceApi.Models;

namespace EcommerceApi.DTOs.Companies
{
    public class CompanyResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Industry { get; set; }
        public string? Address { get; set; }
        public string? LogoUrl { get; set; }
        public CompanyStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}