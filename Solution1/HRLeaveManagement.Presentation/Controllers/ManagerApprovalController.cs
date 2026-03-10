using HRLeaveManagement.Application.DTOs;
using HRLeaveManagement.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HRLeaveManagement.Presentation.Controllers
{
    [Route("api/manager")]
    [ApiController]
    public class ManagerApprovalController : ControllerBase
    {
        private readonly ManagerApprovalService _service;

        public ManagerApprovalController(ManagerApprovalService service)
        {
            _service = service;
        }

        [HttpGet("pendingrequests")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetPendingRequests()
        {
            try
            {
                var requests = await _service.GetPendingRequestsAsync();
                return Ok(requests);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in ManagerApprovalController GetPendingRequests method");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("approve")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Approve([FromBody] ApproveRequestDto dto)
        {
            try
            {
                var leaverequest = await _service.GetByIdAsync(dto);

                if (leaverequest.EmpId == dto.ManagerId)
                    return BadRequest("You cannot approve your own leave request");

                await _service.ApproveAsync(dto);
                return Ok(new { message = "Leave approved successfully" });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in ManagerApprovalController Approve method");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("reject")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Reject([FromBody] RejectRequestDto dto)
        {
            try
            {
                await _service.RejectAsync(dto);
                return Ok(new { message = "Leave rejected successfully" });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in ManagerApprovalController Reject method");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}