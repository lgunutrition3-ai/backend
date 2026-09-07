using Microsoft.EntityFrameworkCore;
using Nutrition_backend.Data;
using Nutrition_backend.DTOs;
using Nutrition_backend.Models;

namespace Nutrition_backend.Services
{
    public interface IAnimalRaisingService
    {
        Task<AnimalRaisingResponseDto> CreateAnimalRaisingAsync(AnimalRaisingEntryDto dto);
        Task<List<AnimalRaisingResponseDto>> GetAnimalRaisingAsync(string barangay, int year);
        Task<AnimalRaisingResponseDto> UpdateAnimalRaisingAsync(int id, AnimalRaisingEntryDto dto);
        Task<bool> DeleteAnimalRaisingAsync(int id);
        Task<bool> DeleteAnimalRaisingManyAsync(List<int> ids);
        Task<bool> CheckAnimalRaisingDuplicateAsync(string householdName, string barangay, int purok, int? excludeId = null);
    }

    public class AnimalRaisingService : IAnimalRaisingService
    {
        private readonly ApplicationDbContext _context;

        public AnimalRaisingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AnimalRaisingResponseDto> CreateAnimalRaisingAsync(AnimalRaisingEntryDto dto)
        {
            var exists = await CheckAnimalRaisingDuplicateAsync(dto.HouseholdName, dto.Barangay, dto.Purok);
            if (exists)
            {
                throw new InvalidOperationException($"A household named '{dto.HouseholdName}' already exists in {dto.Barangay}, Purok {dto.Purok}");
            }

            var report = new AnimalRaisingReport
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
                RecordedDate = dto.RecordedDate
            };

            _context.AnimalRaisingReports.Add(report);
            await _context.SaveChangesAsync();

            return new AnimalRaisingResponseDto
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

        public async Task<AnimalRaisingResponseDto> UpdateAnimalRaisingAsync(int id, AnimalRaisingEntryDto dto)
        {
            var report = await _context.AnimalRaisingReports.FindAsync(id);
            if (report == null)
                throw new KeyNotFoundException($"Record with ID {id} not found");

            var exists = await CheckAnimalRaisingDuplicateAsync(dto.HouseholdName, dto.Barangay, dto.Purok, id);
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

            return new AnimalRaisingResponseDto
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

        public async Task<List<AnimalRaisingResponseDto>> GetAnimalRaisingAsync(string barangay, int year)
        {
            var query = _context.AnimalRaisingReports.AsQueryable();

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

            return reports.Select(r => new AnimalRaisingResponseDto
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

        public async Task<bool> DeleteAnimalRaisingAsync(int id)
        {
            var report = await _context.AnimalRaisingReports.FindAsync(id);
            if (report == null) return false;
            _context.AnimalRaisingReports.Remove(report);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAnimalRaisingManyAsync(List<int> ids)
        {
            if (ids == null || ids.Count == 0) return false;
            var reports = await _context.AnimalRaisingReports.Where(r => ids.Contains(r.Id)).ToListAsync();
            if (reports.Count == 0) return false;
            _context.AnimalRaisingReports.RemoveRange(reports);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckAnimalRaisingDuplicateAsync(string householdName, string barangay, int purok, int? excludeId = null)
        {
            var query = _context.AnimalRaisingReports
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