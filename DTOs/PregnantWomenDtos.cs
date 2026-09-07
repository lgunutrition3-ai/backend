using System.ComponentModel.DataAnnotations;

namespace Nutrition_backend.DTOs
{
    public class PregnantWomenEntryDto
    {
        [Required]
        [MaxLength(100)]
        public string Barangay { get; set; } = string.Empty;

        [Required]
        public int Purok { get; set; }

        [Required]
        public string WomanName { get; set; } = string.Empty;

        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public decimal BMI { get; set; }
        public string? BMICategory { get; set; }
        public int Year { get; set; }
        public DateTime RecordedDate { get; set; }
        public string? RecordedBy { get; set; }
    }

    public class PregnantWomenResponseDto
    {
        public int Id { get; set; }
        public string Barangay { get; set; } = string.Empty;
        public int Purok { get; set; }
        public string WomanName { get; set; } = string.Empty;
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public decimal BMI { get; set; }
        public string? BMICategory { get; set; }
        public int Year { get; set; }
        public string? RecordedBy { get; set; }
        public DateTime RecordedDate { get; set; }
    }
}