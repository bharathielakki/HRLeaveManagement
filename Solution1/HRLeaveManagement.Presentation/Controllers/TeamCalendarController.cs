using HRLeaveManagement.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HRLeaveManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamCalendarController : ControllerBase
    {
        private readonly TeamCalendarService _service;

        public TeamCalendarController(TeamCalendarService service)
        {
            _service = service;
        }

        [HttpGet("team-calendar")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetTeamCalendar(int month, int year, int deptId)
        {
            try
            {
                var result = await _service.GetCalendarAsync(month, year, deptId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in TeamCalendarController GetTeamCalendar method");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}