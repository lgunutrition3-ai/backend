using System.ComponentModel.DataAnnotations;

namespace Nutrition_backend.Models
{
    public class CRReport
    {
        [Key]
        public int Id { get; set; }
        
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
        public DateTime RecordedDate { get; set; } = DateTime.UtcNow;
        
        [MaxLength(100)]
        public string? RecordedBy { get; set; }
    }
}