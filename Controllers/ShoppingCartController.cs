using EcommerceApi.Data;
using EcommerceApi.DTOs.Cart;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EcommerceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Customer,Admin,CompanyAdmin")] 
    public class ShoppingCartController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ShoppingCartController(AppDbContext context)
        {
            _context = context;
        }

        // ==========================
        //  Obtener carrito del usuario autenticado
        // ==========================
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var items = await _context.CartItems
                .Include(ci => ci.Product)
                .Where(ci => ci.UserId == userId)
                .ToListAsync();

            var response = new CartResponseDto
            {
                UserId = userId,
                Items = items.Select(ci => new CartItemResponseDto
                {
                    ProductId = ci.ProductId,
                    ProductName = ci.Product.Name,
                    Price = ci.Product.Price,
                    Quantity = ci.Quantity,
                    AddedAt = ci.AddedAt
                }).ToList(),
                Total = items.Sum(ci => ci.Product.Price * ci.Quantity)
            };

            return Ok(response);
        }

        // ==========================
        //  Agregar producto al carrito
        // ==========================
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var product = await _context.Products.FindAsync(dto.ProductId);
            if (product == null)
                return NotFound(new { message = "Producto no encontrado" });

            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.UserId == userId && ci.ProductId == dto.ProductId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    UserId = userId,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity,
                    AddedAt = DateTime.UtcNow
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += dto.Quantity;
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Producto agregado al carrito" });
        }

        // ==========================
        //  Quitar producto del carrito
        // ==========================
        [HttpPost("remove")]
        public async Task<IActionResult> RemoveFromCart([FromBody] RemoveFromCartDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.UserId == userId && ci.ProductId == dto.ProductId);

            if (cartItem == null)
                return NotFound(new { message = "Producto no encontrado en el carrito" });

            if (cartItem.Quantity <= dto.Quantity)
            {
                _context.CartItems.Remove(cartItem);
            }
            else
            {
                cartItem.Quantity -= dto.Quantity;
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Producto eliminado del carrito" });
        }

        // ==========================
        //  Vaciar carrito
        // ==========================
        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var items = _context.CartItems.Where(ci => ci.UserId == userId);
            _context.CartItems.RemoveRange(items);

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
