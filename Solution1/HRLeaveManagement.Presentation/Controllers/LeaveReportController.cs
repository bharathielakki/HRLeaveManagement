using HRLeaveManagement.Application.DTOs;
using HRLeaveManagement.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HRLeaveManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "HRAdmin")]
    public class LeaveReportController : ControllerBase
    {
        private readonly LeaveReportService _service;

        public LeaveReportController(LeaveReportService service)
        {
            _service = service;
        }

        [HttpPost("report")]
        public async Task<IActionResult> GetReport([FromBody] LeaveReportFilterDto filter)
        {
            try
            {
                var data = await _service.GetLeaveReportAsync(filter);
                return Ok(data);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in LeaveReportController GetReport");
                    
                return StatusCode(500, "Internal server error");
            }
        }
    }
}