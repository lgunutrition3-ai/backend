using System.ComponentModel.DataAnnotations;  // ← ADD THIS LINE

namespace Nutrition_backend.DTOs
{
    public class ReportDto
    {
        [Required]
        [MaxLength(100)]
        public string Barangay { get; set; } = string.Empty;

        [Required]
        public int Purok { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Months6To11 { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Months12To59 { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int UnderweightSUW { get; set; }

        public string? Remarks { get; set; }
        public string? Quarter { get; set; }
        public int? Year { get; set; }
    }
}