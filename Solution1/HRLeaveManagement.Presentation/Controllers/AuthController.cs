using HRLeaveManagement.Application.DTOs;
using HRLeaveManagement.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HRLeaveManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;

        public AuthController(AuthService service)
        {
            _service = service;
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                var token = await _service.LoginAsync(dto);

                return Ok(new { token });
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Error in Auth.Login");
                return StatusCode(500, "Something went wrong");
            }
            
        }
    }
}
