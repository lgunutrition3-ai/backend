using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Nutrition_backend.Data;
using Nutrition_backend.Models;
using Nutrition_backend.DTOs;

namespace Nutrition_backend.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto> SuperAdminLoginAsync(LoginDto loginDto);
        Task<User?> GetUserByIdAsync(int id);
        Task<bool> UserExistsAsync(string username);
        Task ChangePasswordAsync(int userId, string currentPassword, string newPassword);
    }

    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordService _passwordService;
        private readonly IConfiguration _configuration;

        public AuthService(
            ApplicationDbContext context,
            IPasswordService passwordService,
            IConfiguration configuration)
        {
            _context = context;
            _passwordService = passwordService;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            return await LoginBaseAsync(loginDto, null);
        }

        public async Task<AuthResponseDto> SuperAdminLoginAsync(LoginDto loginDto)
        {
            return await LoginBaseAsync(loginDto, "superadmin");
        }

        private async Task<AuthResponseDto> LoginBaseAsync(LoginDto loginDto, string? requiredRole)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == loginDto.Username || u.Email == loginDto.Username);

            if (user == null || !_passwordService.VerifyPassword(loginDto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            if (!user.IsActive)
            {
                throw new UnauthorizedAccessException("Account is deactivated");
            }

            if (requiredRole is null && user.Role == "superadmin")
            {
                throw new UnauthorizedAccessException("This account is not authorized to sign in from this portal");
            }

            if (requiredRole is not null && user.Role != requiredRole)
            {
                throw new UnauthorizedAccessException("This account is not authorized for super admin access");
            }

            var token = GenerateJwtToken(user);

            return new AuthResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                Barangay = user.Barangay,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(24)
            };
        }


        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found");
            }

            if (!_passwordService.VerifyPassword(currentPassword, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Current password is incorrect");
            }

            if (_passwordService.VerifyPassword(newPassword, user.PasswordHash))
            {
                throw new InvalidOperationException("New password must be different from the current password");
            }

            user.PasswordHash = _passwordService.HashPassword(newPassword);
            await _context.SaveChangesAsync();
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured"))
            );
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("Barangay", user.Barangay ?? string.Empty)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}