using Microsoft.EntityFrameworkCore;
using Nutrition_backend.Data;
using Nutrition_backend.DTOs;
using Nutrition_backend.Models;

namespace Nutrition_backend.Services
{
    public interface IPregnantWomenService
    {
        Task<PregnantWomenResponseDto> CreatePregnantWomenAsync(PregnantWomenEntryDto dto);
        Task<List<PregnantWomenResponseDto>> GetPregnantWomenAsync(string barangay, int year);
        Task<PregnantWomenResponseDto> UpdatePregnantWomenAsync(int id, PregnantWomenEntryDto dto);
        Task<bool> DeletePregnantWomenAsync(int id);
        Task<bool> DeletePregnantWomenManyAsync(List<int> ids);
        Task<bool> CheckPregnantWomenDuplicateAsync(string womanName, string barangay, int purok, int? excludeId = null);
    }

    public class PregnantWomenService : IPregnantWomenService
    {
        private readonly ApplicationDbContext _context;

        public PregnantWomenService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PregnantWomenResponseDto> CreatePregnantWomenAsync(PregnantWomenEntryDto dto)
        {
            var exists = await CheckPregnantWomenDuplicateAsync(dto.WomanName, dto.Barangay, dto.Purok);
            if (exists)
            {
                throw new InvalidOperationException($"A woman named '{dto.WomanName}' already exists in {dto.Barangay}, Purok {dto.Purok}");
            }

            var report = new PregnantWomenReport
            {
                Barangay = dto.Barangay,
                Purok = dto.Purok,
                WomanName = dto.WomanName,
                Weight = dto.Weight,
                Height = dto.Height,
                BMI = dto.BMI,
                BMICategory = dto.BMICategory,
                Year = dto.Year,
                RecordedBy = dto.RecordedBy,
                RecordedDate = dto.RecordedDate != DateTime.MinValue ? dto.RecordedDate : DateTime.UtcNow
            };

            _context.PregnantWomenReports.Add(report);
            await _context.SaveChangesAsync();

            return new PregnantWomenResponseDto
            {
                Id = report.Id,
                Barangay = report.Barangay,
                Purok = report.Purok,
                WomanName = report.WomanName,
                Weight = report.Weight,
                Height = report.Height,
                BMI = report.BMI,
                BMICategory = report.BMICategory,
                Year = report.Year,
                RecordedBy = report.RecordedBy,
                RecordedDate = report.RecordedDate
            };
        }

        public async Task<PregnantWomenResponseDto> UpdatePregnantWomenAsync(int id, PregnantWomenEntryDto dto)
        {
            var report = await _context.PregnantWomenReports.FindAsync(id);
            if (report == null)
                throw new KeyNotFoundException($"Record with ID {id} not found");

            var exists = await CheckPregnantWomenDuplicateAsync(dto.WomanName, dto.Barangay, dto.Purok, id);
            if (exists)
            {
                throw new InvalidOperationException($"A woman named '{dto.WomanName}' already exists in {dto.Barangay}, Purok {dto.Purok}");
            }

            report.Barangay = dto.Barangay;
            report.Purok = dto.Purok;
            report.WomanName = dto.WomanName;
            report.Weight = dto.Weight;
            report.Height = dto.Height;
            report.BMI = dto.BMI;
            report.BMICategory = dto.BMICategory;
            report.Year = dto.Year;
            report.RecordedDate = dto.RecordedDate != DateTime.MinValue ? dto.RecordedDate : DateTime.UtcNow;
            report.RecordedBy = dto.RecordedBy;

            await _context.SaveChangesAsync();

            return new PregnantWomenResponseDto
            {
                Id = report.Id,
                Barangay = report.Barangay,
                Purok = report.Purok,
                WomanName = report.WomanName,
                Weight = report.Weight,
                Height = report.Height,
                BMI = report.BMI,
                BMICategory = report.BMICategory,
                Year = report.Year,
                RecordedBy = report.RecordedBy,
                RecordedDate = report.RecordedDate
            };
        }

        public async Task<List<PregnantWomenResponseDto>> GetPregnantWomenAsync(string barangay, int year)
        {
            var query = _context.PregnantWomenReports.AsQueryable();

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

            return reports.Select(r => new PregnantWomenResponseDto
            {
                Id = r.Id,
                Barangay = r.Barangay,
                Purok = r.Purok,
                WomanName = r.WomanName,
                Weight = r.Weight,
                Height = r.Height,
                BMI = r.BMI,
                BMICategory = r.BMICategory,
                Year = r.Year,
                RecordedBy = r.RecordedBy,
                RecordedDate = r.RecordedDate
            }).ToList();
        }

        public async Task<bool> DeletePregnantWomenAsync(int id)
        {
            var report = await _context.PregnantWomenReports.FindAsync(id);
            if (report == null) return false;
            _context.PregnantWomenReports.Remove(report);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePregnantWomenManyAsync(List<int> ids)
        {
            if (ids == null || ids.Count == 0) return false;
            var reports = await _context.PregnantWomenReports.Where(r => ids.Contains(r.Id)).ToListAsync();
            if (reports.Count == 0) return false;
            _context.PregnantWomenReports.RemoveRange(reports);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckPregnantWomenDuplicateAsync(string womanName, string barangay, int purok, int? excludeId = null)
        {
            var query = _context.PregnantWomenReports
                .Where(r => r.WomanName.ToLower() == womanName.ToLower()
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