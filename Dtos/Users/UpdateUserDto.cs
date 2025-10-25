using System.ComponentModel.DataAnnotations;
using EcommerceApi.Models;

namespace EcommerceApi.DTOs.Users
{
    public class UpdateUserDto
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; } = null!;

        public UserRole Role { get; set; } = UserRole.Customer;

        public bool IsActive { get; set; } = true;

        public int? CompanyId { get; set; }
    }
}