using System.ComponentModel.DataAnnotations;

namespace Nutrition_backend.DTOs
{
    public class IodizedSaltEntryDto
    {
        [Required]
        [MaxLength(100)]
        public string Barangay { get; set; } = string.Empty;

        [Required]
        public int Purok { get; set; }

        [MaxLength(200)]
        public string? StoreName { get; set; }

        public bool FineSaltFidel { get; set; }
        public bool FineSaltUFC { get; set; }
        public bool FineSaltPacificBay { get; set; }
        public string? FineSaltOthers { get; set; }
        public bool RockSaltAtlantic { get; set; }
        public bool RockSaltFidel { get; set; }
        public bool RockSaltLasap { get; set; }
        public bool RockSaltPagAsa { get; set; }
        public bool RockSaltJay { get; set; }
        public string? RockSaltOthers { get; set; }
        public bool OilUFC { get; set; }
        public bool OilJolly { get; set; }
        public string? OilOthers { get; set; }
        public DateTime RecordedDate { get; set; }
        public string? RecordedBy { get; set; }
    }

    public class IodizedSaltResponseDto
    {
        public int Id { get; set; }
        public string Barangay { get; set; } = string.Empty;
        public int Purok { get; set; }
        public string? StoreName { get; set; }
        public bool FineSaltFidel { get; set; }
        public bool FineSaltUFC { get; set; }
        public bool FineSaltPacificBay { get; set; }
        public string? FineSaltOthers { get; set; }
        public bool RockSaltAtlantic { get; set; }
        public bool RockSaltFidel { get; set; }
        public bool RockSaltLasap { get; set; }
        public bool RockSaltPagAsa { get; set; }
        public bool RockSaltJay { get; set; }
        public string? RockSaltOthers { get; set; }
        public bool OilUFC { get; set; }
        public bool OilJolly { get; set; }
        public string? OilOthers { get; set; }
        public DateTime RecordedDate { get; set; }
        public string? RecordedBy { get; set; }
        public int Year { get; set; }
    }
}