using System.ComponentModel.DataAnnotations;
using EcommerceApi.Models;

namespace EcommerceApi.DTOs.Companies
{
    public class UpdateCompanyDto
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

        public CompanyStatus Status { get; set; } = CompanyStatus.PendingVerification;
    }
}