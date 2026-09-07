using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nutrition_backend.Models
{
    public class Barangay
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        public bool IsActive { get; set; } = true;
        
        public ICollection<VitaminAReport> Reports { get; set; } = new List<VitaminAReport>();
    }

    public static class BarangayData
    {
        public static readonly List<string> AllBarangays = new()
        {
            "Achila", "Bay-ang", "Benliw", "Biabas", "Bongbong", "Bood", 
            "Buenavista", "Bulilis", "Cagting", "Calanggaman", "California", 
            "Camali-an", "Camambugan", "Casale", "Cuya", "Fatima", "Gabi", 
            "Gov. Boyles", "Guintabo-an", "Hambabauran", "Humayhumay", 
            "Iiihan", "Imelda", "Juagdan", "Katarungan", "Lomangog", 
            "Los Angeles", "Pag-asa", "Pangpang", "Poblacion", 
            "San Francisco", "San Isidro", "San Pascual", "San Vicente", 
            "Sentinela", "Sinandigan", "Tapal", "Tapon", "Tintinan", 
            "Tipolo", "Tubog", "Tuboran", "Union", "Villa Teresita"
        };
    }
}