using HRLeaveManagement.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HRLeaveManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _service;

        public PaymentController(PaymentService service)
        {
            _service = service;
        }

        [HttpGet("myhistory")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetMyPayments(int empId)
        {
            try
            {
                var payments = await _service.GetEmployeePaymentHistoryAsync(empId);
                return Ok(payments);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in PaymentController GetMyPayments method");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}