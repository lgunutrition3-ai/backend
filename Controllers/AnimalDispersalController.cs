using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nutrition_backend.DTOs;
using Nutrition_backend.Services;

namespace Nutrition_backend.Controllers
{
    [ApiController]
    [Route("api/ReportDataEntry/animal-dispersal")]
    [Authorize(Roles = "admin,staff,superadmin")]
    public class AnimalDispersalController : ControllerBase
    {
        private readonly IAnimalDispersalService _service;

        public AnimalDispersalController(IAnimalDispersalService service)
        {
            _service = service;
        }

        [HttpGet("check-duplicate")]
        public async Task<IActionResult> CheckDuplicate([FromQuery] string householdName, [FromQuery] string barangay, [FromQuery] int purok, [FromQuery] int? excludeId = null)
        {
            try
            {
                var exists = await _service.CheckAnimalDispersalDuplicateAsync(householdName, barangay, purok, excludeId);
                return Ok(new { exists });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AnimalDispersalEntryDto dto)
        {
            try
            {
                var result = await _service.CreateAnimalDispersalAsync(dto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{barangay}/{year}")]
        public async Task<IActionResult> GetByBarangay(string barangay, int year)
        {
            try
            {
                var result = await _service.GetAnimalDispersalAsync(barangay, year);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAnimalDispersalAsync(string.Empty, 0);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AnimalDispersalEntryDto dto)
        {
            try
            {
                var result = await _service.UpdateAnimalDispersalAsync(id, dto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
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
                var result = await _service.DeleteAnimalDispersalAsync(id);
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
                var result = await _service.DeleteAnimalDispersalManyAsync(ids);
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