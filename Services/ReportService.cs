using Microsoft.EntityFrameworkCore;
using Nutrition_backend.Data;
using Nutrition_backend.DTOs;
using Nutrition_backend.Models;

namespace Nutrition_backend.Services
{
    public interface IReportService
    {
        Task<BarangayReportDto> GetBarangayReportAsync(string barangay);
        Task<OverallReportDto> GetOverallReportAsync(int year);
        Task<List<ChildRecord>> GetChildRecordsAsync(string? barangay = null);
    }

    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BarangayReportDto> GetBarangayReportAsync(string barangay)
        {
            var records = await _context.ChildRecords
                .Where(r => r.Barangay == barangay)
                .ToListAsync();

            var purokReports = new List<PurokReportDto>();

            for (int p = 1; p <= 7; p++)
            {
                var purokRecords = records.Where(r => r.Purok == p).ToList();
                purokReports.Add(new PurokReportDto
                {
                    Purok = p,
                    Months6To11 = purokRecords.Count(r => r.AgeMonths >= 6 && r.AgeMonths <= 11),
                    Months12To59 = purokRecords.Count(r => r.AgeMonths >= 12 && r.AgeMonths <= 59),
                    UnderweightSUW = purokRecords.Count(r => r.NutritionalStatus == "Underweight" || r.NutritionalStatus == "Severely Underweight")
                });
            }

            var total = new ReportTotalDto
            {
                Months6To11 = purokReports.Sum(p => p.Months6To11),
                Months12To59 = purokReports.Sum(p => p.Months12To59),
                UnderweightSUW = purokReports.Sum(p => p.UnderweightSUW)
            };

            return new BarangayReportDto
            {
                Barangay = barangay,
                PurokReports = purokReports,
                Total = total,
                CertifiedCorrect = "BNS",
                ApprovedBy = "Brgy. Captain"
            };
        }

        public async Task<OverallReportDto> GetOverallReportAsync(int year)
        {
            var records = await _context.ChildRecords
                .Where(r => r.RecordedDate.Year == year)
                .ToListAsync();

            var barangays = records
                .GroupBy(r => r.Barangay)
                .Select(g => new BarangaySummaryDto
                {
                    Barangay = g.Key,
                    Months6To11 = g.Count(r => r.AgeMonths >= 6 && r.AgeMonths <= 11),
                    Months12To59 = g.Count(r => r.AgeMonths >= 12 && r.AgeMonths <= 59),
                    UnderweightSUW = g.Count(r => r.NutritionalStatus == "Underweight" || r.NutritionalStatus == "Severely Underweight")
                })
                .OrderBy(b => b.Barangay)
                .ToList();

            var allBarangays = BarangayData.AllBarangays;
            var finalBarangays = new List<BarangaySummaryDto>();

            foreach (var barangay in allBarangays)
            {
                var existing = barangays.FirstOrDefault(b => b.Barangay == barangay);
                finalBarangays.Add(new BarangaySummaryDto
                {
                    Barangay = barangay,
                    Months6To11 = existing?.Months6To11 ?? 0,
                    Months12To59 = existing?.Months12To59 ?? 0,
                    UnderweightSUW = existing?.UnderweightSUW ?? 0
                });
            }

            var overallTotal = new OverallTotalDto
            {
                Months6To11 = finalBarangays.Sum(b => b.Months6To11),
                Months12To59 = finalBarangays.Sum(b => b.Months12To59),
                UnderweightSUW = finalBarangays.Sum(b => b.UnderweightSUW),
                TotalBarangays = finalBarangays.Count(b => b.Months6To11 > 0 || b.Months12To59 > 0 || b.UnderweightSUW > 0)
            };

            return new OverallReportDto
            {
                Year = year.ToString(),
                Barangays = finalBarangays,
                OverallTotal = overallTotal,
                PreparedBy = "Cristine A. Macahis, MNPC",
                NotedBy = "Jehd Stephen O. Cutamora, RN"
            };
        }

        public async Task<List<ChildRecord>> GetChildRecordsAsync(string? barangay = null)
        {
            var query = _context.ChildRecords.AsQueryable();
            if (!string.IsNullOrEmpty(barangay))
            {
                query = query.Where(r => r.Barangay == barangay);
            }
            return await query.OrderByDescending(r => r.RecordedDate).ToListAsync();
        }
    }
}