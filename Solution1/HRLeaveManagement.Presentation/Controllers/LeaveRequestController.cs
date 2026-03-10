using HRLeaveManagement.Application.DTOs;
using HRLeaveManagement.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HRLeaveManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly LeaveRequestService _service;

        public LeaveRequestController(LeaveRequestService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Submit([FromBody] LeaveRequestDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var id = await _service.SubmitAsync(dto);
                return Ok(new { RequestId = id, message = "Leave request submitted successfully" });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in LeaveRequestController Submit method");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("myrequests/{empId}")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> MyRequests(int empId)
        {
            try
            {
                var requests = await _service.GetMyRequestsAsync(empId);
                return Ok(requests);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in LeaveRequestController MyRequests method");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("cancel/{empId}/{requestId}")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Cancel(int empId, int requestId)
        {
            try
            {
                await _service.CancelAsync(requestId, empId);
                return Ok(new { message = "Leave request cancelled successfully" });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in LeaveRequestController Cancel method");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}