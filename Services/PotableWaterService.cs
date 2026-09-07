using Microsoft.EntityFrameworkCore;
using Nutrition_backend.Data;
using Nutrition_backend.DTOs;
using Nutrition_backend.Models;

namespace Nutrition_backend.Services
{
    public interface IPotableWaterService
    {
        Task<PotableWaterResponseDto> CreatePotableWaterAsync(PotableWaterEntryDto dto);
        Task<List<PotableWaterResponseDto>> GetPotableWaterAsync(string barangay, int year);
        Task<PotableWaterResponseDto> UpdatePotableWaterAsync(int id, PotableWaterEntryDto dto);
        Task<bool> DeletePotableWaterAsync(int id);
        Task<bool> DeletePotableWaterManyAsync(List<int> ids);
        Task<bool> CheckPotableWaterDuplicateAsync(string householdName, string barangay, int purok, int? excludeId = null);
    }

    public class PotableWaterService : IPotableWaterService
    {
        private readonly ApplicationDbContext _context;

        public PotableWaterService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PotableWaterResponseDto> CreatePotableWaterAsync(PotableWaterEntryDto dto)
        {
            var exists = await CheckPotableWaterDuplicateAsync(dto.HouseholdName, dto.Barangay, dto.Purok);
            if (exists)
            {
                throw new InvalidOperationException($"A household named '{dto.HouseholdName}' already exists in {dto.Barangay}, Purok {dto.Purok}");
            }

            var report = new PotableWaterReport
            {
                Barangay = dto.Barangay,
                Purok = dto.Purok,
                HouseholdName = dto.HouseholdName,
                Level1 = dto.Level1,
                Level2 = dto.Level2,
                Level3 = dto.Level3,
                Year = dto.Year,
                RecordedBy = dto.RecordedBy,
                RecordedDate = dto.RecordedDate != DateTime.MinValue ? dto.RecordedDate : DateTime.UtcNow
            };

            _context.PotableWaterReports.Add(report);
            await _context.SaveChangesAsync();

            return new PotableWaterResponseDto
            {
                Id = report.Id,
                Barangay = report.Barangay,
                Purok = report.Purok,
                HouseholdName = report.HouseholdName,
                Level1 = report.Level1,
                Level2 = report.Level2,
                Level3 = report.Level3,
                Year = report.Year,
                RecordedBy = report.RecordedBy,
                RecordedDate = report.RecordedDate
            };
        }

        public async Task<PotableWaterResponseDto> UpdatePotableWaterAsync(int id, PotableWaterEntryDto dto)
        {
            var report = await _context.PotableWaterReports.FindAsync(id);
            if (report == null)
                throw new KeyNotFoundException($"Record with ID {id} not found");

            var exists = await CheckPotableWaterDuplicateAsync(dto.HouseholdName, dto.Barangay, dto.Purok, id);
            if (exists)
            {
                throw new InvalidOperationException($"A household named '{dto.HouseholdName}' already exists in {dto.Barangay}, Purok {dto.Purok}");
            }

            report.Barangay = dto.Barangay;
            report.Purok = dto.Purok;
            report.HouseholdName = dto.HouseholdName;
            report.Level1 = dto.Level1;
            report.Level2 = dto.Level2;
            report.Level3 = dto.Level3;
            report.Year = dto.Year;
            report.RecordedDate = dto.RecordedDate != DateTime.MinValue ? dto.RecordedDate : DateTime.UtcNow;
            report.RecordedBy = dto.RecordedBy;

            await _context.SaveChangesAsync();

            return new PotableWaterResponseDto
            {
                Id = report.Id,
                Barangay = report.Barangay,
                Purok = report.Purok,
                HouseholdName = report.HouseholdName,
                Level1 = report.Level1,
                Level2 = report.Level2,
                Level3 = report.Level3,
                Year = report.Year,
                RecordedBy = report.RecordedBy,
                RecordedDate = report.RecordedDate
            };
        }

        public async Task<List<PotableWaterResponseDto>> GetPotableWaterAsync(string barangay, int year)
        {
            var query = _context.PotableWaterReports.AsQueryable();

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

            return reports.Select(r => new PotableWaterResponseDto
            {
                Id = r.Id,
                Barangay = r.Barangay,
                Purok = r.Purok,
                HouseholdName = r.HouseholdName,
                Level1 = r.Level1,
                Level2 = r.Level2,
                Level3 = r.Level3,
                Year = r.Year,
                RecordedBy = r.RecordedBy,
                RecordedDate = r.RecordedDate
            }).ToList();
        }

        public async Task<bool> DeletePotableWaterAsync(int id)
        {
            var report = await _context.PotableWaterReports.FindAsync(id);
            if (report == null) return false;
            _context.PotableWaterReports.Remove(report);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePotableWaterManyAsync(List<int> ids)
        {
            if (ids == null || ids.Count == 0) return false;
            var reports = await _context.PotableWaterReports.Where(r => ids.Contains(r.Id)).ToListAsync();
            if (reports.Count == 0) return false;
            _context.PotableWaterReports.RemoveRange(reports);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckPotableWaterDuplicateAsync(string householdName, string barangay, int purok, int? excludeId = null)
        {
            var query = _context.PotableWaterReports
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