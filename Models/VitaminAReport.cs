using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nutrition_backend.Models
{
    public class VitaminAReport
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Barangay { get; set; } = string.Empty;
        
        public int Purok { get; set; } // Null for barangay total, 1-7 for purok details
        
        [Required]
        [Range(0, int.MaxValue)]
        public int Months6To11 { get; set; }
        
        [Required]
        [Range(0, int.MaxValue)]
        public int Months12To59 { get; set; }
        
        [Required]
        [Range(0, int.MaxValue)]
        public int UnderweightSUW { get; set; }
        
        [Required]
        public int ReportedByUserId { get; set; }
        
        [ForeignKey("ReportedByUserId")]
        public User? ReportedBy { get; set; }
        
        public DateTime ReportedDate { get; set; } = DateTime.UtcNow;
        
        [MaxLength(50)]
        public string Status { get; set; } = "pending";
        
        public DateTime? ApprovedDate { get; set; }
        
        public int? ApprovedByUserId { get; set; }
        
        [ForeignKey("ApprovedByUserId")]
        public User? ApprovedBy { get; set; }
        
        public string? Remarks { get; set; }
        
        public string? Quarter { get; set; }
        public int Year { get; set; } = DateTime.UtcNow.Year;
        
        [NotMapped]
        public int TotalChildren => Months6To11 + Months12To59 + UnderweightSUW;
        
        public bool IsBarangayTotal => Purok == 0;
    }
}