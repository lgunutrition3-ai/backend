using System.Collections.Generic;

namespace Nutrition_backend.DTOs
{
    public class OverallReportDto
    {
        public string Year { get; set; } = DateTime.UtcNow.Year.ToString();
        public List<BarangaySummaryDto> Barangays { get; set; } = new();
        public OverallTotalDto OverallTotal { get; set; } = new();
        public string PreparedBy { get; set; } = string.Empty;
        public string NotedBy { get; set; } = string.Empty;
    }

    public class BarangaySummaryDto
    {
        public string Barangay { get; set; } = string.Empty;
        public int Months6To11 { get; set; }
        public int Months12To59 { get; set; }
        public int UnderweightSUW { get; set; }
        public int Total => Months6To11 + Months12To59 + UnderweightSUW;
    }

    public class OverallTotalDto
    {
        public int Months6To11 { get; set; }
        public int Months12To59 { get; set; }
        public int UnderweightSUW { get; set; }
        public int GrandTotal => Months6To11 + Months12To59 + UnderweightSUW;
        public int TotalBarangays { get; set; }
    }
}