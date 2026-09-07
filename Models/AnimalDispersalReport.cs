using System.ComponentModel.DataAnnotations;

namespace Nutrition_backend.Models
{
    public class AnimalDispersalReport
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
        public int ChickenMale { get; set; }
        public int ChickenFemale { get; set; }
        
        // Pig
        public int PigMale { get; set; }
        public int PigFemale { get; set; }
        
        // Goat
        public int GoatMale { get; set; }
        public int GoatFemale { get; set; }
        
        // Cow
        public int CowMale { get; set; }
        public int CowFemale { get; set; }
        
        // Carabao
        public int CarabaoMale { get; set; }
        public int CarabaoFemale { get; set; }
        
        // Other
        public int OtherMale { get; set; }
        public int OtherFemale { get; set; }
        
        public int Year { get; set; }
        public DateTime RecordedDate { get; set; } = DateTime.UtcNow;
        
        [MaxLength(100)]
        public string? RecordedBy { get; set; }
    }
}