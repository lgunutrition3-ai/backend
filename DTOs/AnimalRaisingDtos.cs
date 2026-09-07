using System.ComponentModel.DataAnnotations;

namespace Nutrition_backend.DTOs
{
    public class AnimalRaisingEntryDto
    {
        [Required]
        [MaxLength(100)]
        public string Barangay { get; set; } = string.Empty;

        [Required]
        public int Purok { get; set; }

        [Required]
        public string HouseholdName { get; set; } = string.Empty;

        public int ChickenMale { get; set; }
        public int ChickenFemale { get; set; }
        public int PigMale { get; set; }
        public int PigFemale { get; set; }
        public int GoatMale { get; set; }
        public int GoatFemale { get; set; }
        public int CowMale { get; set; }
        public int CowFemale { get; set; }
        public int CarabaoMale { get; set; }
        public int CarabaoFemale { get; set; }
        public int OtherMale { get; set; }
        public int OtherFemale { get; set; }

        public int Year { get; set; }
        public DateTime RecordedDate { get; set; }
        public string? RecordedBy { get; set; }
    }

    public class AnimalRaisingResponseDto
    {
        public int Id { get; set; }
        public string Barangay { get; set; } = string.Empty;
        public int Purok { get; set; }
        public string HouseholdName { get; set; } = string.Empty;
        public int ChickenMale { get; set; }
        public int ChickenFemale { get; set; }
        public int PigMale { get; set; }
        public int PigFemale { get; set; }
        public int GoatMale { get; set; }
        public int GoatFemale { get; set; }
        public int CowMale { get; set; }
        public int CowFemale { get; set; }
        public int CarabaoMale { get; set; }
        public int CarabaoFemale { get; set; }
        public int OtherMale { get; set; }
        public int OtherFemale { get; set; }
        public int Year { get; set; }
        public string? RecordedBy { get; set; }
        public DateTime RecordedDate { get; set; }
    }
}