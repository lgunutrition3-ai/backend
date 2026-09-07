using System.ComponentModel.DataAnnotations;

namespace Nutrition_backend.DTOs
{
    public class CREntryDto
    {
        [Required]
        [MaxLength(100)]
        public string Barangay { get; set; } = string.Empty;

        [Required]
        public int Purok { get; set; }

        [Required]
        public string HouseholdName { get; set; } = string.Empty;

        public bool WithCR { get; set; }
        public bool WithoutCR { get; set; }
        public int Year { get; set; }
        public DateTime RecordedDate { get; set; }
        public string? RecordedBy { get; set; }
    }

    public class CRResponseDto
    {
        public int Id { get; set; }
        public string Barangay { get; set; } = string.Empty;
        public int Purok { get; set; }
        public string HouseholdName { get; set; } = string.Empty;
        public bool WithCR { get; set; }
        public bool WithoutCR { get; set; }
        public int Year { get; set; }
        public string? RecordedBy { get; set; }
        public DateTime RecordedDate { get; set; }
    }
}