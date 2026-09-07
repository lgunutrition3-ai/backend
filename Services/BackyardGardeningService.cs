using Microsoft.EntityFrameworkCore;
using Nutrition_backend.Data;
using Nutrition_backend.DTOs;
using Nutrition_backend.Models;

namespace Nutrition_backend.Services
{
    public interface IBackyardGardeningService
    {
        Task<BackyardGardeningResponseDto> CreateBackyardGardeningAsync(BackyardGardeningEntryDto dto);
        Task<List<BackyardGardeningResponseDto>> GetBackyardGardeningAsync(string barangay, int year);
        Task<BackyardGardeningResponseDto> UpdateBackyardGardeningAsync(int id, BackyardGardeningEntryDto dto);
        Task<bool> DeleteBackyardGardeningAsync(int id);
        Task<bool> DeleteBackyardGardeningManyAsync(List<int> ids);
        Task<bool> CheckBackyardGardeningDuplicateAsync(string householdName, string barangay, int purok, int? excludeId = null);
    }

    public class BackyardGardeningService : IBackyardGardeningService
    {
        private readonly ApplicationDbContext _context;

        public BackyardGardeningService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BackyardGardeningResponseDto> CreateBackyardGardeningAsync(BackyardGardeningEntryDto dto)
        {
            var exists = await CheckBackyardGardeningDuplicateAsync(dto.HouseholdName, dto.Barangay, dto.Purok);
            if (exists)
            {
                throw new InvalidOperationException($"A household named '{dto.HouseholdName}' already exists in {dto.Barangay}, Purok {dto.Purok}");
            }

            var report = new BackyardGardeningReport
            {
                Barangay = dto.Barangay,
                Purok = dto.Purok,
                HouseholdName = dto.HouseholdName,
                HasGarden = dto.HasGarden,
                Year = dto.Year,
                RecordedBy = dto.RecordedBy,
                RecordedDate = dto.RecordedDate != DateTime.MinValue ? dto.RecordedDate : DateTime.UtcNow
            };

            _context.BackyardGardeningReports.Add(report);
            await _context.SaveChangesAsync();

            return new BackyardGardeningResponseDto
            {
                Id = report.Id,
                Barangay = report.Barangay,
                Purok = report.Purok,
                HouseholdName = report.HouseholdName,
                HasGarden = report.HasGarden,
                Year = report.Year,
                RecordedBy = report.RecordedBy,
                RecordedDate = report.RecordedDate
            };
        }

        public async Task<BackyardGardeningResponseDto> UpdateBackyardGardeningAsync(int id, BackyardGardeningEntryDto dto)
        {
            var report = await _context.BackyardGardeningReports.FindAsync(id);
            if (report == null)
                throw new KeyNotFoundException($"Record with ID {id} not found");

            var exists = await CheckBackyardGardeningDuplicateAsync(dto.HouseholdName, dto.Barangay, dto.Purok, id);
            if (exists)
            {
                throw new InvalidOperationException($"A household named '{dto.HouseholdName}' already exists in {dto.Barangay}, Purok {dto.Purok}");
            }

            report.Barangay = dto.Barangay;
            report.Purok = dto.Purok;
            report.HouseholdName = dto.HouseholdName;
            report.HasGarden = dto.HasGarden;
            report.Year = dto.Year;
            report.RecordedDate = dto.RecordedDate != DateTime.MinValue ? dto.RecordedDate : DateTime.UtcNow;
            report.RecordedBy = dto.RecordedBy;

            await _context.SaveChangesAsync();

            return new BackyardGardeningResponseDto
            {
                Id = report.Id,
                Barangay = report.Barangay,
                Purok = report.Purok,
                HouseholdName = report.HouseholdName,
                HasGarden = report.HasGarden,
                Year = report.Year,
                RecordedBy = report.RecordedBy,
                RecordedDate = report.RecordedDate
            };
        }

        public async Task<List<BackyardGardeningResponseDto>> GetBackyardGardeningAsync(string barangay, int year)
        {
            var query = _context.BackyardGardeningReports.AsQueryable();

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

            return reports.Select(r => new BackyardGardeningResponseDto
            {
                Id = r.Id,
                Barangay = r.Barangay,
                Purok = r.Purok,
                HouseholdName = r.HouseholdName,
                HasGarden = r.HasGarden,
                Year = r.Year,
                RecordedBy = r.RecordedBy,
                RecordedDate = r.RecordedDate
            }).ToList();
        }

        public async Task<bool> DeleteBackyardGardeningAsync(int id)
        {
            var report = await _context.BackyardGardeningReports.FindAsync(id);
            if (report == null) return false;
            _context.BackyardGardeningReports.Remove(report);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBackyardGardeningManyAsync(List<int> ids)
        {
            if (ids == null || ids.Count == 0) return false;
            var reports = await _context.BackyardGardeningReports.Where(r => ids.Contains(r.Id)).ToListAsync();
            if (reports.Count == 0) return false;
            _context.BackyardGardeningReports.RemoveRange(reports);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckBackyardGardeningDuplicateAsync(string householdName, string barangay, int purok, int? excludeId = null)
        {
            var query = _context.BackyardGardeningReports
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