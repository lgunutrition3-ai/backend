using System.ComponentModel.DataAnnotations;

namespace Nutrition_backend.Models
{
    public class PregnantWomenReport
{
    [Key]
    public int Id { get; set; }
    
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
    public DateTime RecordedDate { get; set; } = DateTime.UtcNow;
    
    [MaxLength(100)]
    public string? RecordedBy { get; set; }
}
}