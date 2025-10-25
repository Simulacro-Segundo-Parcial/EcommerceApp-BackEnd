using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.DTOs.Users
{
    public class LoginDto   
    {
        [Required, EmailAddress, MaxLength(500)]
        public string Email { get; set; } = null!;

        [Required, MinLength(6), MaxLength(100)]
        public string Password { get; set; } = null!;
    }
}