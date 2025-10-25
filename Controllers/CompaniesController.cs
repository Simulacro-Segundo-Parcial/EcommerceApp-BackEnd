using EcommerceApi.Data;
using EcommerceApi.DTOs.Companies;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,CompanyAdmin")] 
    public class CompaniesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CompaniesController(AppDbContext context)
        {
            _context = context;
        }

        // ==========================
        // Crear empresa
        // ==========================
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCompanyDto dto)
        {
            var company = new Company
            {
                Name = dto.Name,
                Description = dto.Description,
                Industry = dto.Industry,
                Address = dto.Address,
                LogoUrl = dto.LogoUrl,
                Status = CompanyStatus.PendingVerification
            };

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            var response = MapToResponse(company);
            return CreatedAtAction(nameof(GetById), new { id = company.Id }, response);
        }

        // ==========================
        //  Obtener todas las empresas
        // ==========================
        [HttpGet]
        [AllowAnonymous] // Permitir que cualquiera consulte empresas
        public async Task<IActionResult> GetAll()
        {
            var companies = await _context.Companies.ToListAsync();
            var response = companies.Select(MapToResponse);
            return Ok(response);
        }

        // ==========================
        //  Obtener empresa por Id
        // ==========================
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
                return NotFound(new { message = "Empresa no encontrada" });

            return Ok(MapToResponse(company));
        }

        // ==========================
        //  Actualizar empresa
        // ==========================
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCompanyDto dto)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
                return NotFound(new { message = "Empresa no encontrada" });

            company.Name = dto.Name;
            company.Description = dto.Description;
            company.Industry = dto.Industry;
            company.Address = dto.Address;
            company.LogoUrl = dto.LogoUrl;
            company.Status = dto.Status;

            await _context.SaveChangesAsync();

            return Ok(MapToResponse(company));
        }

        // ==========================
        //  Eliminar empresa
        // ==========================
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
                return NotFound(new { message = "Empresa no encontrada" });

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ==========================
        //  Mapper interno
        // ==========================
        private CompanyResponseDto MapToResponse(Company company)
        {
            return new CompanyResponseDto
            {
                Id = company.Id,
                Name = company.Name,
                Description = company.Description,
                Industry = company.Industry,
                Address = company.Address,
                LogoUrl = company.LogoUrl,
                Status = company.Status,
                CreatedAt = company.CreatedAt
            };
        }
    }
}
