using Microsoft.EntityFrameworkCore;
using Nutrition_backend.Data;
using Nutrition_backend.DTOs;
using Nutrition_backend.Models;

namespace Nutrition_backend.Services
{
    public interface ICRService
    {
        Task<CRResponseDto> CreateCRAsync(CREntryDto dto);
        Task<List<CRResponseDto>> GetCRAsync(string barangay, int year);
        Task<CRResponseDto> UpdateCRAsync(int id, CREntryDto dto);
        Task<bool> DeleteCRAsync(int id);
        Task<bool> DeleteCRManyAsync(List<int> ids);
        Task<bool> CheckCRDuplicateAsync(string householdName, string barangay, int purok, int? excludeId = null);
    }

    public class CRService : ICRService
    {
        private readonly ApplicationDbContext _context;

        public CRService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CRResponseDto> CreateCRAsync(CREntryDto dto)
        {
            var exists = await CheckCRDuplicateAsync(dto.HouseholdName, dto.Barangay, dto.Purok);
            if (exists)
            {
                throw new InvalidOperationException($"A household named '{dto.HouseholdName}' already exists in {dto.Barangay}, Purok {dto.Purok}");
            }

            var report = new CRReport
            {
                Barangay = dto.Barangay,
                Purok = dto.Purok,
                HouseholdName = dto.HouseholdName,
                WithCR = dto.WithCR,
                WithoutCR = dto.WithoutCR,
                Year = dto.Year,
                RecordedBy = dto.RecordedBy,
                RecordedDate = dto.RecordedDate != DateTime.MinValue ? dto.RecordedDate : DateTime.UtcNow
            };

            _context.CRReports.Add(report);
            await _context.SaveChangesAsync();

            return new CRResponseDto
            {
                Id = report.Id,
                Barangay = report.Barangay,
                Purok = report.Purok,
                HouseholdName = report.HouseholdName,
                WithCR = report.WithCR,
                WithoutCR = report.WithoutCR,
                Year = report.Year,
                RecordedBy = report.RecordedBy,
                RecordedDate = report.RecordedDate
            };
        }

        public async Task<CRResponseDto> UpdateCRAsync(int id, CREntryDto dto)
        {
            var report = await _context.CRReports.FindAsync(id);
            if (report == null)
                throw new KeyNotFoundException($"Record with ID {id} not found");

            var exists = await CheckCRDuplicateAsync(dto.HouseholdName, dto.Barangay, dto.Purok, id);
            if (exists)
            {
                throw new InvalidOperationException($"A household named '{dto.HouseholdName}' already exists in {dto.Barangay}, Purok {dto.Purok}");
            }

            report.Barangay = dto.Barangay;
            report.Purok = dto.Purok;
            report.HouseholdName = dto.HouseholdName;
            report.WithCR = dto.WithCR;
            report.WithoutCR = dto.WithoutCR;
            report.Year = dto.Year;
            report.RecordedDate = dto.RecordedDate != DateTime.MinValue ? dto.RecordedDate : DateTime.UtcNow;
            report.RecordedBy = dto.RecordedBy;

            await _context.SaveChangesAsync();

            return new CRResponseDto
            {
                Id = report.Id,
                Barangay = report.Barangay,
                Purok = report.Purok,
                HouseholdName = report.HouseholdName,
                WithCR = report.WithCR,
                WithoutCR = report.WithoutCR,
                Year = report.Year,
                RecordedBy = report.RecordedBy,
                RecordedDate = report.RecordedDate
            };
        }

        public async Task<List<CRResponseDto>> GetCRAsync(string barangay, int year)
        {
            var query = _context.CRReports.AsQueryable();
            if (!string.IsNullOrEmpty(barangay))
            {
                query = query.Where(r => r.Barangay == barangay);
            }

            if (year != 0)
            {
                query = query.Where(r => r.Year == year);
            }

            var reports = await query
                .OrderBy(r => r.Purok)
                .ToListAsync();

            return reports.Select(r => new CRResponseDto
            {
                Id = r.Id,
                Barangay = r.Barangay,
                Purok = r.Purok,
                HouseholdName = r.HouseholdName,
                WithCR = r.WithCR,
                WithoutCR = r.WithoutCR,
                Year = r.Year,
                RecordedBy = r.RecordedBy,
                RecordedDate = r.RecordedDate
            }).ToList();
        }

        public async Task<bool> DeleteCRAsync(int id)
        {
            var report = await _context.CRReports.FindAsync(id);
            if (report == null) return false;
            _context.CRReports.Remove(report);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCRManyAsync(List<int> ids)
        {
            if (ids == null || ids.Count == 0) return false;
            var reports = await _context.CRReports.Where(r => ids.Contains(r.Id)).ToListAsync();
            if (reports.Count == 0) return false;
            _context.CRReports.RemoveRange(reports);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckCRDuplicateAsync(string householdName, string barangay, int purok, int? excludeId = null)
        {
            var query = _context.CRReports
                .Where(r => r.HouseholdName.ToLower() == householdName.ToLower()
                    && r.Barangay == barangay
                    && r.Purok == purok);

            if (excludeId.HasValue)
            {
                query = query.Where(r => r.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }
    }
}