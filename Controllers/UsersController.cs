using EcommerceApi.DTOs.Users;
using EcommerceApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,CompanyAdmin")] // Solo Admins y CompanyAdmins pueden gestionar usuarios
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // ==========================
        //  Obtener todos los usuarios
        // ==========================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        // ==========================
        //  Obtener usuario por Id
        // ==========================
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound(new { message = "Usuario no encontrado" });

            return Ok(user);
        }

        // ==========================
        //  Actualizar usuario
        // ==========================
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
        {
            var updatedUser = await _userService.UpdateAsync(id, dto);
            if (updatedUser == null)
                return NotFound(new { message = "Usuario no encontrado" });

            return Ok(updatedUser);
        }

        // ==========================
        //  Eliminar usuario
        // ==========================
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _userService.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { message = "Usuario no encontrado" });

            return NoContent();
        }
    }
}
