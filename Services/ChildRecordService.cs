using Microsoft.EntityFrameworkCore;
using Nutrition_backend.Data;
using Nutrition_backend.DTOs;
using Nutrition_backend.Models;

namespace Nutrition_backend.Services
{
    public interface IChildRecordService
    {
        Task<ChildRecord> CreateAsync(ChildRecordDto dto, int userId);
        Task<List<ChildRecord>> GetAllAsync();
        Task<ChildRecord?> GetByIdAsync(int id);
        Task<ChildRecord> UpdateAsync(int id, ChildRecordDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteManyAsync(List<int> ids);
        Task<bool> CheckDuplicateAsync(string fullName, string barangay, int purok, int? excludeId = null);
    }

    public class ChildRecordService : IChildRecordService
    {
        private readonly ApplicationDbContext _context;

        public ChildRecordService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ChildRecord> CreateAsync(ChildRecordDto dto, int userId)
        {
            // ✅ userId is passed from the controller
            var record = new ChildRecord
            {
                Barangay = dto.Barangay,
                Purok = dto.Purok,
                TargetCategory = dto.TargetCategory,
                FullName = dto.FullName,
                MotherOrCaregiver = dto.MotherOrCaregiver,
                Sex = dto.Sex,
                Birthdate = dto.Birthdate,
                AgeMonths = dto.AgeMonths,
                Weight = dto.Weight,
                Height = dto.Height,
                NutritionalStatus = dto.NutritionalStatus,
                RecordedBy = userId,  // ← This must be a valid User ID
                RecordedDate = dto.RecordedDate != DateTime.MinValue ? dto.RecordedDate : DateTime.UtcNow, 
            };

            _context.ChildRecords.Add(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<List<ChildRecord>> GetAllAsync()
        {
            return await _context.ChildRecords
                .Include(r => r.User)
                .OrderByDescending(r => r.RecordedDate)
                .ToListAsync();
        }

        public async Task<bool> CheckDuplicateAsync(string fullName, string barangay, int purok, int? excludeId = null)
        {
            var query = _context.ChildRecords
                .Where(r => r.FullName.ToLower() == fullName.ToLower() 
                    && r.Barangay == barangay 
                    && r.Purok == purok);
            
            if (excludeId.HasValue)
            {
                query = query.Where(r => r.Id != excludeId.Value);
            }
            
            return await query.AnyAsync();
        }

        public async Task<ChildRecord?> GetByIdAsync(int id)
        {
            return await _context.ChildRecords
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<ChildRecord> UpdateAsync(int id, ChildRecordDto dto)
        {
            var record = await GetByIdAsync(id);
            if (record == null)
                throw new KeyNotFoundException($"Record with ID {id} not found");

            record.Barangay = dto.Barangay;
            record.Purok = dto.Purok;
            record.TargetCategory = dto.TargetCategory;
            record.FullName = dto.FullName;
            record.MotherOrCaregiver = dto.MotherOrCaregiver;
            record.Sex = dto.Sex;
            record.Birthdate = dto.Birthdate;
            record.AgeMonths = dto.AgeMonths;
            record.Weight = dto.Weight;
            record.Height = dto.Height;
            record.NutritionalStatus = dto.NutritionalStatus;

            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var record = await _context.ChildRecords.FindAsync(id);
            if (record == null)
                return false;

            _context.ChildRecords.Remove(record);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteManyAsync(List<int> ids)
        {
            if (ids == null || ids.Count == 0)
                return false;

            var records = await _context.ChildRecords.Where(r => ids.Contains(r.Id)).ToListAsync();
            if (records.Count == 0)
                return false;

            _context.ChildRecords.RemoveRange(records);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}