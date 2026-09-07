using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nutrition_backend.DTOs;
using Nutrition_backend.Services;

namespace Nutrition_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("barangay/{barangay}")]
        public async Task<IActionResult> GetBarangayReport(string barangay)
        {
            try
            {
                var report = await _reportService.GetBarangayReportAsync(barangay);
                return Ok(report);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("overall")]
        [Authorize(Roles = "admin,superadmin")]
        public async Task<IActionResult> GetOverallReport([FromQuery] int? year)
        {
            try
            {
                var targetYear = year ?? DateTime.UtcNow.Year;
                var report = await _reportService.GetOverallReportAsync(targetYear);
                return Ok(report);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("child-records")]
        public async Task<IActionResult> GetChildRecords([FromQuery] string? barangay = null)
        {
            try
            {
                var records = await _reportService.GetChildRecordsAsync(barangay);
                return Ok(records);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}