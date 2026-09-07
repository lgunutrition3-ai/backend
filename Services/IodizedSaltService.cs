using Microsoft.EntityFrameworkCore;
using Nutrition_backend.Data;
using Nutrition_backend.DTOs;
using Nutrition_backend.Models;

namespace Nutrition_backend.Services
{
    public interface IIodizedSaltService
    {
        Task<IodizedSaltResponseDto> CreateIodizedSaltAsync(IodizedSaltEntryDto dto);
        Task<List<IodizedSaltResponseDto>> GetIodizedSaltAsync(string barangay, int year);
        Task<IodizedSaltResponseDto> UpdateIodizedSaltAsync(int id, IodizedSaltEntryDto dto);
        Task<bool> DeleteIodizedSaltAsync(int id);
        Task<bool> DeleteIodizedSaltManyAsync(List<int> ids);
        Task<bool> CheckIodizedSaltDuplicateAsync(string? storeName, string barangay, int purok, int? excludeId = null);
    }

    public class IodizedSaltService : IIodizedSaltService
    {
        private readonly ApplicationDbContext _context;

        public IodizedSaltService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IodizedSaltResponseDto> CreateIodizedSaltAsync(IodizedSaltEntryDto dto)
        {
            var exists = await CheckIodizedSaltDuplicateAsync(dto.StoreName, dto.Barangay, dto.Purok);
            if (exists)
            {
                throw new InvalidOperationException($"A store named '{dto.StoreName}' already exists in {dto.Barangay}, Purok {dto.Purok}");
            }

            var report = new IodizedSaltReport
            {
                Barangay = dto.Barangay,
                Purok = dto.Purok,
                StoreName = dto.StoreName,
                FineSaltFidel = dto.FineSaltFidel,
                FineSaltUFC = dto.FineSaltUFC,
                FineSaltPacificBay = dto.FineSaltPacificBay,
                FineSaltOthers = dto.FineSaltOthers,
                RockSaltAtlantic = dto.RockSaltAtlantic,
                RockSaltFidel = dto.RockSaltFidel,
                RockSaltLasap = dto.RockSaltLasap,
                RockSaltPagAsa = dto.RockSaltPagAsa,
                RockSaltJay = dto.RockSaltJay,
                RockSaltOthers = dto.RockSaltOthers,
                OilUFC = dto.OilUFC,
                OilJolly = dto.OilJolly,
                OilOthers = dto.OilOthers,
                RecordedDate = dto.RecordedDate != DateTime.MinValue ? dto.RecordedDate : DateTime.UtcNow,
                Year = dto.RecordedDate.Year
            };

            _context.IodizedSaltReports.Add(report);
            await _context.SaveChangesAsync();

            return new IodizedSaltResponseDto
            {
                Id = report.Id,
                Barangay = report.Barangay,
                Purok = report.Purok,
                StoreName = report.StoreName,
                FineSaltFidel = report.FineSaltFidel,
                FineSaltUFC = report.FineSaltUFC,
                FineSaltPacificBay = report.FineSaltPacificBay,
                FineSaltOthers = report.FineSaltOthers,
                RockSaltAtlantic = report.RockSaltAtlantic,
                RockSaltFidel = report.RockSaltFidel,
                RockSaltLasap = report.RockSaltLasap,
                RockSaltPagAsa = report.RockSaltPagAsa,
                RockSaltJay = report.RockSaltJay,
                RockSaltOthers = report.RockSaltOthers,
                OilUFC = report.OilUFC,
                OilJolly = report.OilJolly,
                OilOthers = report.OilOthers,
                RecordedDate = report.RecordedDate,
                Year = report.Year
            };
        }

        public async Task<IodizedSaltResponseDto> UpdateIodizedSaltAsync(int id, IodizedSaltEntryDto dto)
        {
            var report = await _context.IodizedSaltReports.FindAsync(id);
            if (report == null)
                throw new KeyNotFoundException($"Record with ID {id} not found");

            var exists = await CheckIodizedSaltDuplicateAsync(dto.StoreName, dto.Barangay, dto.Purok, id);
            if (exists)
            {
                throw new InvalidOperationException($"A store named '{dto.StoreName}' already exists in {dto.Barangay}, Purok {dto.Purok}");
            }

            report.Barangay = dto.Barangay;
            report.Purok = dto.Purok;
            report.StoreName = dto.StoreName;
            report.FineSaltFidel = dto.FineSaltFidel;
            report.FineSaltUFC = dto.FineSaltUFC;
            report.FineSaltPacificBay = dto.FineSaltPacificBay;
            report.FineSaltOthers = dto.FineSaltOthers;
            report.RockSaltAtlantic = dto.RockSaltAtlantic;
            report.RockSaltFidel = dto.RockSaltFidel;
            report.RockSaltLasap = dto.RockSaltLasap;
            report.RockSaltPagAsa = dto.RockSaltPagAsa;
            report.RockSaltJay = dto.RockSaltJay;
            report.RockSaltOthers = dto.RockSaltOthers;
            report.OilUFC = dto.OilUFC;
            report.OilJolly = dto.OilJolly;
            report.OilOthers = dto.OilOthers;
            report.RecordedDate = dto.RecordedDate != DateTime.MinValue ? dto.RecordedDate : DateTime.UtcNow;
            report.RecordedBy = dto.RecordedBy;
            report.Year = report.RecordedDate.Year;

            await _context.SaveChangesAsync();

            return new IodizedSaltResponseDto
            {
                Id = report.Id,
                Barangay = report.Barangay,
                Purok = report.Purok,
                StoreName = report.StoreName,
                FineSaltFidel = report.FineSaltFidel,
                FineSaltUFC = report.FineSaltUFC,
                FineSaltPacificBay = report.FineSaltPacificBay,
                FineSaltOthers = report.FineSaltOthers,
                RockSaltAtlantic = report.RockSaltAtlantic,
                RockSaltFidel = report.RockSaltFidel,
                RockSaltLasap = report.RockSaltLasap,
                RockSaltPagAsa = report.RockSaltPagAsa,
                RockSaltJay = report.RockSaltJay,
                RockSaltOthers = report.RockSaltOthers,
                OilUFC = report.OilUFC,
                OilJolly = report.OilJolly,
                OilOthers = report.OilOthers,
                Year = report.Year,
                RecordedBy = report.RecordedBy,
                RecordedDate = report.RecordedDate
            };
        }

        public async Task<List<IodizedSaltResponseDto>> GetIodizedSaltAsync(string barangay, int year)
        {
            var query = _context.IodizedSaltReports.AsQueryable();

            if (!string.IsNullOrEmpty(barangay))
            {
                query = query.Where(r => r.Barangay == barangay);
            }

            if (year != 0)
            {
                query = query.Where(r => r.Year == year);
            }

            var reports = await query
                .OrderByDescending(r => r.RecordedDate)
                .ToListAsync();

            return reports.Select(r => new IodizedSaltResponseDto
            {
                Id = r.Id,
                Barangay = r.Barangay,
                Purok = r.Purok,
                StoreName = r.StoreName,
                FineSaltFidel = r.FineSaltFidel,
                FineSaltUFC = r.FineSaltUFC,
                FineSaltPacificBay = r.FineSaltPacificBay,
                FineSaltOthers = r.FineSaltOthers,
                RockSaltAtlantic = r.RockSaltAtlantic,
                RockSaltFidel = r.RockSaltFidel,
                RockSaltLasap = r.RockSaltLasap,
                RockSaltPagAsa = r.RockSaltPagAsa,
                RockSaltJay = r.RockSaltJay,
                RockSaltOthers = r.RockSaltOthers,
                OilUFC = r.OilUFC,
                OilJolly = r.OilJolly,
                OilOthers = r.OilOthers,
                RecordedDate = r.RecordedDate,
                RecordedBy = r.RecordedBy,
                Year = r.Year
            }).ToList();
        }

        public async Task<bool> DeleteIodizedSaltAsync(int id)
        {
            var report = await _context.IodizedSaltReports.FindAsync(id);
            if (report == null) return false;
            _context.IodizedSaltReports.Remove(report);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteIodizedSaltManyAsync(List<int> ids)
        {
            if (ids == null || ids.Count == 0) return false;
            var reports = await _context.IodizedSaltReports.Where(r => ids.Contains(r.Id)).ToListAsync();
            if (reports.Count == 0) return false;
            _context.IodizedSaltReports.RemoveRange(reports);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckIodizedSaltDuplicateAsync(string? storeName, string barangay, int purok, int? excludeId = null)
        {
            var query = _context.IodizedSaltReports
                .Where(r => (r.StoreName ?? string.Empty).ToLower() == (storeName ?? string.Empty).ToLower()
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