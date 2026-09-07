using System.ComponentModel.DataAnnotations;

namespace Nutrition_backend.Models
{
    public class VegetableSeedReport
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
        
        public string? SeedTypes { get; set; }
        
        public string? Beneficiaries { get; set; }
        
        public int Year { get; set; }
        public DateTime RecordedDate { get; set; } = DateTime.UtcNow;
        
        [MaxLength(100)]
        public string? RecordedBy { get; set; }
    }
}