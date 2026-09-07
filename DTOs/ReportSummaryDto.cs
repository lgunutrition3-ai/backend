using System.Collections.Generic;

namespace Nutrition_backend.DTOs
{
    public class ReportSummaryDto
    {
        public string Barangay { get; set; } = string.Empty;
        public int TotalReports { get; set; }
        public int TotalMonths6To11 { get; set; }
        public int TotalMonths12To59 { get; set; }
        public int TotalUnderweightSUW { get; set; }
        public int TotalChildren { get; set; }
        public List<PurokSummaryDto> ReportsByPurok { get; set; } = new();
    }

    public class PurokSummaryDto
    {
        public int Purok { get; set; }
        public int TotalChildren { get; set; }
        public int Count { get; set; }
    }
}