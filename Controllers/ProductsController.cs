using EcommerceApi.Data;
using EcommerceApi.DTOs.Products;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,CompanyAdmin")] 
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // ==========================
        //  Crear producto
        // ==========================
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                ImageUrl = dto.ImageUrl,
                CompanyId = dto.CompanyId,
                Status = ProductStatus.Draft
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, MapToResponse(product));
        }

        // ==========================
        //  Obtener todos los productos
        // ==========================
        [HttpGet]
        [AllowAnonymous] // Público: todos pueden ver productos
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products.ToListAsync();
            var response = products.Select(MapToResponse);
            return Ok(response);
        }

        // ==========================
        //  Obtener producto por Id
        // ==========================
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound(new { message = "Producto no encontrado" });

            return Ok(MapToResponse(product));
        }

        // ==========================
        //  Actualizar producto
        // ==========================
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound(new { message = "Producto no encontrado" });

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Stock = dto.Stock;
            product.ImageUrl = dto.ImageUrl;
            product.Status = dto.Status;

            await _context.SaveChangesAsync();

            return Ok(MapToResponse(product));
        }

        // ==========================
        //  Publicar producto
        // ==========================
        [HttpPost("{id:int}/publish")]
        public async Task<IActionResult> Publish(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound(new { message = "Producto no encontrado" });

            product.Status = ProductStatus.Published;
            product.PublishedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(MapToResponse(product));
        }

        // ==========================
        //  Eliminar producto
        // ==========================
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound(new { message = "Producto no encontrado" });

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ==========================
        //  Mapper interno
        // ==========================
        private ProductResponseDto MapToResponse(Product product)
        {
            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Status = product.Status,
                ImageUrl = product.ImageUrl,
                CompanyId = product.CompanyId,
                CreatedAt = product.CreatedAt,
                PublishedAt = product.PublishedAt
            };
        }
    }
}
