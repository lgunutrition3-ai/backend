using System.ComponentModel.DataAnnotations;

namespace Nutrition_backend.DTOs
{
    public class ChildRecordDto
    {
        [Required]
        [MaxLength(100)]
        public string Barangay { get; set; } = string.Empty;

        [Required]
        public int Purok { get; set; }

        [Required]
        [MaxLength(50)]
        public string TargetCategory { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? MotherOrCaregiver { get; set; }

        [MaxLength(10)]
        public string? Sex { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        public int AgeMonths { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal Weight { get; set; }

        [Required]
        [Range(0, 200)]
        public decimal Height { get; set; }

        [Required]
        [MaxLength(50)]
        public string NutritionalStatus { get; set; } = string.Empty;
        
        [Required]
        public DateTime RecordedDate { get; set; } = DateTime.UtcNow;
    }

    public class CheckDuplicateDto
    {
        public string FullName { get; set; } = String.Empty;
        public string Barangay { get; set; } = String.Empty;
        public int Purok { get; set; }
        public int? ExcludeId { get; set; }
    }
}