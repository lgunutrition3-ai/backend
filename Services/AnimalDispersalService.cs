using Microsoft.EntityFrameworkCore;
using Nutrition_backend.Data;
using Nutrition_backend.DTOs;
using Nutrition_backend.Models;

namespace Nutrition_backend.Services
{
    public interface IAnimalDispersalService
    {
        Task<AnimalDispersalResponseDto> CreateAnimalDispersalAsync(AnimalDispersalEntryDto dto);
        Task<List<AnimalDispersalResponseDto>> GetAnimalDispersalAsync(string barangay, int year);
        Task<AnimalDispersalResponseDto> UpdateAnimalDispersalAsync(int id, AnimalDispersalEntryDto dto);
        Task<bool> DeleteAnimalDispersalAsync(int id);
        Task<bool> DeleteAnimalDispersalManyAsync(List<int> ids);
        Task<bool> CheckAnimalDispersalDuplicateAsync(string householdName, string barangay, int purok, int? excludeId = null);
    }

    public class AnimalDispersalService : IAnimalDispersalService
    {
        private readonly ApplicationDbContext _context;

        public AnimalDispersalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AnimalDispersalResponseDto> CreateAnimalDispersalAsync(AnimalDispersalEntryDto dto)
        {
            var exists = await CheckAnimalDispersalDuplicateAsync(dto.HouseholdName, dto.Barangay, dto.Purok);
            if (exists)
            {
                throw new InvalidOperationException($"A household named '{dto.HouseholdName}' already exists in {dto.Barangay}, Purok {dto.Purok}");
            }

            var report = new AnimalDispersalReport
            {
                Barangay = dto.Barangay,
                Purok = dto.Purok,
                HouseholdName = dto.HouseholdName,
                ChickenMale = dto.ChickenMale,
                ChickenFemale = dto.ChickenFemale,
                PigMale = dto.PigMale,
                PigFemale = dto.PigFemale,
                GoatMale = dto.GoatMale,
                GoatFemale = dto.GoatFemale,
                CowMale = dto.CowMale,
                CowFemale = dto.CowFemale,
                CarabaoMale = dto.CarabaoMale,
                CarabaoFemale = dto.CarabaoFemale,
                OtherMale = dto.OtherMale,
                OtherFemale = dto.OtherFemale,
                Year = dto.Year,
                RecordedBy = dto.RecordedBy,
                RecordedDate = dto.RecordedDate != DateTime.MinValue ? dto.RecordedDate : DateTime.UtcNow
            };

            _context.AnimalDispersalReports.Add(report);
            await _context.SaveChangesAsync();

            return new AnimalDispersalResponseDto
            {
                Id = report.Id,
                Barangay = report.Barangay,
                Purok = report.Purok,
                HouseholdName = report.HouseholdName,
                ChickenMale = report.ChickenMale,
                ChickenFemale = report.ChickenFemale,
                PigMale = report.PigMale,
                PigFemale = report.PigFemale,
                GoatMale = report.GoatMale,
                GoatFemale = report.GoatFemale,
                CowMale = report.CowMale,
                CowFemale = report.CowFemale,
                CarabaoMale = report.CarabaoMale,
                CarabaoFemale = report.CarabaoFemale,
                OtherMale = report.OtherMale,
                OtherFemale = report.OtherFemale,
                Year = report.Year,
                RecordedBy = report.RecordedBy,
                RecordedDate = report.RecordedDate
            };
        }

        public async Task<AnimalDispersalResponseDto> UpdateAnimalDispersalAsync(int id, AnimalDispersalEntryDto dto)
        {
            var report = await _context.AnimalDispersalReports.FindAsync(id);
            if (report == null)
                throw new KeyNotFoundException($"Record with ID {id} not found");

            var exists = await CheckAnimalDispersalDuplicateAsync(dto.HouseholdName, dto.Barangay, dto.Purok, id);
            if (exists)
            {
                throw new InvalidOperationException($"A household named '{dto.HouseholdName}' already exists in {dto.Barangay}, Purok {dto.Purok}");
            }

            report.Barangay = dto.Barangay;
            report.Purok = dto.Purok;
            report.HouseholdName = dto.HouseholdName;
            report.ChickenMale = dto.ChickenMale;
            report.ChickenFemale = dto.ChickenFemale;
            report.PigMale = dto.PigMale;
            report.PigFemale = dto.PigFemale;
            report.GoatMale = dto.GoatMale;
            report.GoatFemale = dto.GoatFemale;
            report.CowMale = dto.CowMale;
            report.CowFemale = dto.CowFemale;
            report.CarabaoMale = dto.CarabaoMale;
            report.CarabaoFemale = dto.CarabaoFemale;
            report.OtherMale = dto.OtherMale;
            report.OtherFemale = dto.OtherFemale;
            report.Year = dto.Year;
            report.RecordedDate = dto.RecordedDate != DateTime.MinValue ? dto.RecordedDate : DateTime.UtcNow;
            report.RecordedBy = dto.RecordedBy;

            await _context.SaveChangesAsync();

            return new AnimalDispersalResponseDto
            {
                Id = report.Id,
                Barangay = report.Barangay,
                Purok = report.Purok,
                HouseholdName = report.HouseholdName,
                ChickenMale = report.ChickenMale,
                ChickenFemale = report.ChickenFemale,
                PigMale = report.PigMale,
                PigFemale = report.PigFemale,
                GoatMale = report.GoatMale,
                GoatFemale = report.GoatFemale,
                CowMale = report.CowMale,
                CowFemale = report.CowFemale,
                CarabaoMale = report.CarabaoMale,
                CarabaoFemale = report.CarabaoFemale,
                OtherMale = report.OtherMale,
                OtherFemale = report.OtherFemale,
                Year = report.Year,
                RecordedBy = report.RecordedBy,
                RecordedDate = report.RecordedDate
            };
        }

        public async Task<List<AnimalDispersalResponseDto>> GetAnimalDispersalAsync(string barangay, int year)
        {
            var query = _context.AnimalDispersalReports.AsQueryable();
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

            return reports.Select(r => new AnimalDispersalResponseDto
            {
                Id = r.Id,
                Barangay = r.Barangay,
                Purok = r.Purok,
                HouseholdName = r.HouseholdName,
                ChickenMale = r.ChickenMale,
                ChickenFemale = r.ChickenFemale,
                PigMale = r.PigMale,
                PigFemale = r.PigFemale,
                GoatMale = r.GoatMale,
                GoatFemale = r.GoatFemale,
                CowMale = r.CowMale,
                CowFemale = r.CowFemale,
                CarabaoMale = r.CarabaoMale,
                CarabaoFemale = r.CarabaoFemale,
                OtherMale = r.OtherMale,
                OtherFemale = r.OtherFemale,
                Year = r.Year,
                RecordedBy = r.RecordedBy,
                RecordedDate = r.RecordedDate
            }).ToList();
        }

        public async Task<bool> DeleteAnimalDispersalAsync(int id)
        {
            var report = await _context.AnimalDispersalReports.FindAsync(id);
            if (report == null) return false;
            _context.AnimalDispersalReports.Remove(report);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAnimalDispersalManyAsync(List<int> ids)
        {
            if (ids == null || ids.Count == 0) return false;
            var reports = await _context.AnimalDispersalReports.Where(r => ids.Contains(r.Id)).ToListAsync();
            if (reports.Count == 0) return false;
            _context.AnimalDispersalReports.RemoveRange(reports);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckAnimalDispersalDuplicateAsync(string householdName, string barangay, int purok, int? excludeId = null)
        {
            var query = _context.AnimalDispersalReports
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