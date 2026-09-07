using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Nutrition_backend.DTOs;
using Nutrition_backend.Services;

namespace Nutrition_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChildRecordsController : ControllerBase
    {
        private readonly IChildRecordService _childRecordService;

        public ChildRecordsController(IChildRecordService childRecordService)
        {
            _childRecordService = childRecordService;
        }

        private int GetCurrentUserId()
        {
            // Try multiple claim types
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)
                ?? User.FindFirst("nameid")
                ?? User.FindFirst("id")
                ?? User.FindFirst("sub")
                ?? User.FindFirst("userId");

            if (userIdClaim == null)
            {
                // Log all claims for debugging
                Console.WriteLine("=== ALL CLAIMS ===");
                foreach (var claim in User.Claims)
                {
                    Console.WriteLine($"Claim: {claim.Type} = {claim.Value}");
                }
                throw new UnauthorizedAccessException("No user ID claim found in token");
            }

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new UnauthorizedAccessException($"Invalid user ID format: {userIdClaim.Value}");
            }

            Console.WriteLine($"✅ User ID extracted: {userId}");
            return userId;
        }

        [HttpPost("check-duplicate")]
public async Task<IActionResult> CheckDuplicate([FromBody] CheckDuplicateDto dto)
{
    try
    {
        var exists = await _childRecordService.CheckDuplicateAsync(
            dto.FullName, 
            dto.Barangay, 
            dto.Purok, 
            dto.ExcludeId
        );
        return Ok(new { exists });
    }
    catch (Exception ex)
    {
        return BadRequest(new { message = ex.Message });
    }
}

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ChildRecordDto dto)
        {
            try
            {
                var userId = GetCurrentUserId();
                Console.WriteLine($"📝 Creating record for user ID: {userId}");
                
                var record = await _childRecordService.CreateAsync(dto, userId);
                return Ok(record);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var records = await _childRecordService.GetAllAsync();
                return Ok(records);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var record = await _childRecordService.GetByIdAsync(id);
                if (record == null)
                    return NotFound(new { message = "Record not found" });
                return Ok(record);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ChildRecordDto dto)
        {
            try
            {
                var record = await _childRecordService.UpdateAsync(id, dto);
                return Ok(record);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _childRecordService.DeleteAsync(id);
                if (!result)
                    return NotFound(new { message = "Record not found" });
                return Ok(new { message = "Record deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("batch-delete")]
        public async Task<IActionResult> BatchDelete([FromBody] List<int> ids)
        {
            try
            {
                var result = await _childRecordService.DeleteManyAsync(ids);
                if (!result)
                    return NotFound(new { message = "No records found" });
                return Ok(new { message = "Records deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}