using EcommerceApi.DTOs.Users;

namespace EcommerceApi.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = null!;
        public DateTime Expiration { get; set; }
        public UserResponseDto User { get; set; } = null!;
    }
}