using System.ComponentModel.DataAnnotations;

namespace Nutrition_backend.DTOs
{
    public class UpdateAdminDto
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string? NewPassword { get; set; }
    }
}
