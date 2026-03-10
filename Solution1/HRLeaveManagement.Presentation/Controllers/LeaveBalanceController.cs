using HRLeaveManagement.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HRLeaveManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveBalanceController : ControllerBase
    {
        private readonly LeaveBalanceService _service;

        public LeaveBalanceController(LeaveBalanceService service)
        {
            _service = service;
        }

        [HttpGet("mybalance")]
        [Authorize(Roles = "Employee,Manager")]
        public async Task<IActionResult> GetMyLeaveBalance(int empId, int year)
        {
            try
            {
                var balances = await _service.GetMyLeaveBalanceAsync(empId, year);
                return Ok(balances);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in LeaveBalanceController GetMyLeaveBalance ");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}