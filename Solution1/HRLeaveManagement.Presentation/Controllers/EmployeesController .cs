using HRLeaveManagement.Application.DTOs;
using HRLeaveManagement.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HRLeaveManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _service;

        public EmployeesController(EmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "HRAdmin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EmployeesController GetAll");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "HRAdmin,Manager")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var emp = await _service.GetByIdAsync(id);

                if (emp == null)
                    return NotFound();

                return Ok(emp);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EmployeesController GetById with Id {EmployeeId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeDto dto)
        {
            try
            {
                var id = await _service.CreateAsync(dto);
                return Ok("Employee Added Successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EmployeesController Create");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "HRAdmin")]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeDto dto)
        {
            try
            {
                await _service.UpdateAsync(id, dto);
                return Ok("Employee Updated Successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EmployeesController Update with Id {EmployeeId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "HRAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok("Employee Deleted Successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EmployeesController Delete with Id {EmployeeId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("Department")]
        [Authorize(Roles = "HRAdmin")]
        public async Task<IActionResult> GetDepartment()
        {
            try
            {
                var result = await _service.GetDepartmentAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EmployeesController GetDepartment");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("managers")]
        [Authorize(Roles = "HRAdmin")]
        public async Task<IActionResult> GetManagers()
        {
            try
            {
                var managers = await _service.GetManagersAsync();
                return Ok(managers);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EmployeesController GetManagers");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}