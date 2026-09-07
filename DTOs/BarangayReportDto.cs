using System.Collections.Generic;

namespace Nutrition_backend.DTOs
{
    public class BarangayReportDto
    {
        public string Barangay { get; set; } = string.Empty;
        public List<PurokReportDto> PurokReports { get; set; } = new();
        public ReportTotalDto Total { get; set; } = new();
        public string CertifiedCorrect { get; set; } = string.Empty;
        public string ApprovedBy { get; set; } = string.Empty;
    }

    public class PurokReportDto
    {
        public int Purok { get; set; }
        public int Months6To11 { get; set; }
        public int Months12To59 { get; set; }
        public int UnderweightSUW { get; set; }
        public int Total => Months6To11 + Months12To59 + UnderweightSUW;
    }

    public class ReportTotalDto
    {
        public int Months6To11 { get; set; }
        public int Months12To59 { get; set; }
        public int UnderweightSUW { get; set; }
        public int GrandTotal => Months6To11 + Months12To59 + UnderweightSUW;
    }
}