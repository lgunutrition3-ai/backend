using System.ComponentModel.DataAnnotations;

namespace Nutrition_backend.DTOs
{
    public class BackyardGardeningEntryDto
    {
        [Required]
        [MaxLength(100)]
        public string Barangay { get; set; } = string.Empty;

        [Required]
        public int Purok { get; set; }

        [Required]
        public string HouseholdName { get; set; } = string.Empty;

        public bool HasGarden { get; set; }
        public int Year { get; set; }
        public DateTime RecordedDate { get; set; }
        public string? RecordedBy { get; set; }
    }

    public class BackyardGardeningResponseDto
    {
        public int Id { get; set; }
        public string Barangay { get; set; } = string.Empty;
        public int Purok { get; set; }
        public string HouseholdName { get; set; } = string.Empty;
        public bool HasGarden { get; set; }
        public int Year { get; set; }
        public string? RecordedBy { get; set; }
        public DateTime RecordedDate { get; set; }
    }
}