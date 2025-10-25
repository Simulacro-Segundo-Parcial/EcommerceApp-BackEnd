using EcommerceApi.Models;

namespace EcommerceApi.DTOs.Users
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
        public int? CompanyId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}