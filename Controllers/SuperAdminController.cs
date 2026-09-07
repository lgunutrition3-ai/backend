using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nutrition_backend.Data;
using Nutrition_backend.DTOs;
using Nutrition_backend.Models;
using Nutrition_backend.Services;

namespace Nutrition_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "superadmin")]
    public class SuperAdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordService _passwordService;

        public SuperAdminController(ApplicationDbContext context, IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        [HttpGet("admins")]
        public async Task<IActionResult> GetAdmins()
        {
            var admins = await _context.Users
                .Where(u => u.Role == "admin")
                .Select(u => new
                {
                    u.Id,
                    u.Username,
                    u.Email,
                    u.CreatedAt,
                    u.IsActive
                })
                .OrderBy(u => u.Id)
                .ToListAsync();

            return Ok(admins);
        }

        [HttpPost("admins")]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateAdminDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
            {
                return BadRequest(new { message = "Username already exists" });
            }

            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            {
                return BadRequest(new { message = "Email already exists" });
            }

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = _passwordService.HashPassword(dto.Password),
                Role = "admin",
                Barangay = null,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                user.Id,
                user.Username,
                user.Email,
                user.CreatedAt
            });
        }

        [HttpPut("admins/{id}/toggle")]
        public async Task<IActionResult> ToggleAdminStatus(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null || user.Role != "admin")
            {
                return NotFound(new { message = "Admin not found" });
            }

            user.IsActive = !user.IsActive;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                user.Id,
                user.IsActive,
                message = user.IsActive ? "Admin activated" : "Admin deactivated"
            });
        }

        [HttpDelete("admins/{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null || user.Role != "admin")
            {
                return NotFound(new { message = "Admin not found" });
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Admin deleted successfully" });
        }
    }
}