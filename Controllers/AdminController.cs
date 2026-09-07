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
    [Authorize(Roles = "admin,superadmin")]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordService _passwordService;

        public AdminController(ApplicationDbContext context, IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        [HttpGet("staff")]
        public async Task<IActionResult> GetStaff()
        {
            var staff = await _context.Users
                .Where(u => u.Role == "staff")
                .Select(u => new
                {
                    u.Id,
                    u.Username,
                    u.Email,
                    u.Barangay,
                    u.CreatedAt,
                    u.IsActive
                })
                .ToListAsync();

            return Ok(staff);
        }

        [HttpPost("staff")]
        public async Task<IActionResult> CreateStaff([FromBody] CreateStaffDto dto)
        {
            // Check if username exists
            if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
            {
                return BadRequest(new { message = "Username already exists" });
            }

            // Check if email exists
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            {
                return BadRequest(new { message = "Email already exists" });
            }

            // Check if barangay exists
            if (!BarangayData.AllBarangays.Contains(dto.Barangay))
            {
                return BadRequest(new { message = "Invalid barangay" });
            }

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = _passwordService.HashPassword(dto.Password),
                Role = "staff",
                Barangay = dto.Barangay,
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
                user.Barangay,
                user.CreatedAt
            });
        }

        [HttpPut("staff/{id}/toggle")]
        public async Task<IActionResult> ToggleStaffStatus(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null || user.Role != "staff")
            {
                return NotFound(new { message = "Staff not found" });
            }

            user.IsActive = !user.IsActive;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                user.Id,
                user.IsActive,
                message = user.IsActive ? "Staff activated" : "Staff deactivated"
            });
        }

        [HttpDelete("staff/{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null || user.Role != "staff")
            {
                return NotFound(new { message = "Staff not found" });
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Staff deleted successfully" });
        }
    }
}