using EcommerceApi.Data;
using EcommerceApi.DTOs.Users;
using EcommerceApi.DTOs.Auth;
using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace EcommerceApi.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;

        public UserService(AppDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        // ==========================
        //  Registro
        // ==========================
        public async Task<UserResponseDto> RegisterAsync(RegisterUserDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                throw new Exception("El email ya está registrado.");

            var user = new User
            {
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password),
                FullName = dto.FullName,
                Role = dto.Role,
                CompanyId = dto.CompanyId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return MapToResponse(user);
        }

        // ==========================
        //  Login
        // ==========================
        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
                return null;

            var token = _tokenService.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(2),
                User = MapToResponse(user)
            };
        }

        // ==========================
        //  Obtener por Id
        // ==========================
        public async Task<UserResponseDto?> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user == null ? null : MapToResponse(user);
        }

        // ==========================
        // Obtener todos
        // ==========================
        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users.Select(MapToResponse);
        }

        // ==========================
        //  Actualizar
        // ==========================
        public async Task<UserResponseDto?> UpdateAsync(int id, UpdateUserDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            user.FullName = dto.FullName;
            user.Role = dto.Role;
            user.IsActive = dto.IsActive;
            user.CompanyId = dto.CompanyId;

            await _context.SaveChangesAsync();

            return MapToResponse(user);
        }

        // ==========================
        //  Eliminar
        // ==========================
        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        // ==========================
        //  Métodos auxiliares
        // ==========================
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }

        private UserResponseDto MapToResponse(User user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role,
                IsActive = user.IsActive,
                CompanyId = user.CompanyId,
                CreatedAt = user.CreatedAt
            };
        }
    }
}
