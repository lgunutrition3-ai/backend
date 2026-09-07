using Microsoft.EntityFrameworkCore;
using Nutrition_backend.Data;
using Nutrition_backend.DTOs;
using Nutrition_backend.Models;

namespace Nutrition_backend.Services
{
    public interface IVegetableSeedService
    {
        Task<VegetableSeedResponseDto> CreateVegetableSeedAsync(VegetableSeedEntryDto dto);
        Task<List<VegetableSeedResponseDto>> GetVegetableSeedAsync(string barangay, int year);
        Task<VegetableSeedResponseDto> UpdateVegetableSeedAsync(int id, VegetableSeedEntryDto dto);
        Task<bool> DeleteVegetableSeedAsync(int id);
        Task<bool> DeleteVegetableSeedManyAsync(List<int> ids);
        Task<bool> CheckVegetableSeedDuplicateAsync(string householdName, string barangay, int purok, int? excludeId = null);
    }

    public class VegetableSeedService : IVegetableSeedService
    {
        private readonly ApplicationDbContext _context;

        public VegetableSeedService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<VegetableSeedResponseDto> CreateVegetableSeedAsync(VegetableSeedEntryDto dto)
        {
            var exists = await CheckVegetableSeedDuplicateAsync(dto.HouseholdName, dto.Barangay, dto.Purok);
            if (exists)
            {
                throw new InvalidOperationException($"A household named '{dto.HouseholdName}' already exists in {dto.Barangay}, Purok {dto.Purok}");
            }

            var report = new VegetableSeedReport
            {
                Barangay = dto.Barangay,
                Purok = dto.Purok,
                HouseholdName = dto.HouseholdName,
                SeedTypes = dto.SeedTypes,
                Beneficiaries = dto.Beneficiaries,
                Year = dto.Year,
                RecordedBy = dto.RecordedBy,
                RecordedDate = dto.RecordedDate != DateTime.MinValue ? dto.RecordedDate : DateTime.UtcNow
            };

            _context.VegetableSeedReports.Add(report);
            await _context.SaveChangesAsync();

            return new VegetableSeedResponseDto
            {
                Id = report.Id,
                Barangay = report.Barangay,
                Purok = report.Purok,
                HouseholdName = report.HouseholdName,
                SeedTypes = report.SeedTypes,
                Beneficiaries = report.Beneficiaries,
                Year = report.Year,
                RecordedBy = report.RecordedBy,
                RecordedDate = report.RecordedDate
            };
        }

        public async Task<VegetableSeedResponseDto> UpdateVegetableSeedAsync(int id, VegetableSeedEntryDto dto)
        {
            var report = await _context.VegetableSeedReports.FindAsync(id);
            if (report == null)
                throw new KeyNotFoundException($"Record with ID {id} not found");

            var exists = await CheckVegetableSeedDuplicateAsync(dto.HouseholdName, dto.Barangay, dto.Purok, id);
            if (exists)
            {
                throw new InvalidOperationException($"A household named '{dto.HouseholdName}' already exists in {dto.Barangay}, Purok {dto.Purok}");
            }

            report.Barangay = dto.Barangay;
            report.Purok = dto.Purok;
            report.HouseholdName = dto.HouseholdName;
            report.SeedTypes = dto.SeedTypes;
            report.Beneficiaries = dto.Beneficiaries;
            report.Year = dto.Year;
            report.RecordedDate = dto.RecordedDate != DateTime.MinValue ? dto.RecordedDate : DateTime.UtcNow;
            report.RecordedBy = dto.RecordedBy;

            await _context.SaveChangesAsync();

            return new VegetableSeedResponseDto
            {
                Id = report.Id,
                Barangay = report.Barangay,
                Purok = report.Purok,
                HouseholdName = report.HouseholdName,
                SeedTypes = report.SeedTypes,
                Beneficiaries = report.Beneficiaries,
                Year = report.Year,
                RecordedBy = report.RecordedBy,
                RecordedDate = report.RecordedDate
            };
        }

        public async Task<List<VegetableSeedResponseDto>> GetVegetableSeedAsync(string barangay, int year)
        {
            var query = _context.VegetableSeedReports.AsQueryable();

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

            return reports.Select(r => new VegetableSeedResponseDto
            {
                Id = r.Id,
                Barangay = r.Barangay,
                Purok = r.Purok,
                HouseholdName = r.HouseholdName,
                SeedTypes = r.SeedTypes,
                Beneficiaries = r.Beneficiaries,
                Year = r.Year,
                RecordedBy = r.RecordedBy,
                RecordedDate = r.RecordedDate
            }).ToList();
        }

        public async Task<bool> DeleteVegetableSeedAsync(int id)
        {
            var report = await _context.VegetableSeedReports.FindAsync(id);
            if (report == null)
                return false;

            _context.VegetableSeedReports.Remove(report);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteVegetableSeedManyAsync(List<int> ids)
        {
            if (ids == null || ids.Count == 0) return false;
            var reports = await _context.VegetableSeedReports.Where(r => ids.Contains(r.Id)).ToListAsync();
            if (reports.Count == 0) return false;
            _context.VegetableSeedReports.RemoveRange(reports);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckVegetableSeedDuplicateAsync(string householdName, string barangay, int purok, int? excludeId = null)
        {
            var query = _context.VegetableSeedReports
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