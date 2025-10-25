using System.ComponentModel.DataAnnotations;
using EcommerceApi.Models;

namespace EcommerceApi.DTOs.Users
{
    public class RegisterUserDto
    {
        [Required, EmailAddress, MaxLength(500)]
        public string Email { get; set; } = null!;

        [Required, MinLength(6), MaxLength(100)]
        public string Password { get; set; } = null!;

        [Required, MaxLength(100)]
        public string FullName { get; set; } = null!;

        public UserRole Role { get; set; } = UserRole.Customer;

        public int? CompanyId { get; set; }
    }
}