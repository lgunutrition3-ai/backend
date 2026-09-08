using System.ComponentModel.DataAnnotations;
using Nutrition_backend.Helpers;

namespace Nutrition_backend.DTOs
{
    public class ChangePasswordDto
    {
        [Required]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required]
        [StrongPassword]
        public string NewPassword { get; set; } = string.Empty;
    }
}