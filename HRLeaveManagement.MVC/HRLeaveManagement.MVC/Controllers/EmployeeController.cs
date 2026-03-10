using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;

namespace HRLeaveManagement.MVC.Controllers
{
   
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Dashboard()
            {
            try
            {
                var token = HttpContext.Session.GetString("JWToken");
                var empId = int.Parse(HttpContext.Session.GetString("EmpId"));

                var requests = await _employeeService.GetMyRequests(empId, token);
                var balances = await _employeeService.GetMyBalance(empId, token);

                ViewBag.Balances = balances;

                return View(requests);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EmployeeController Dashboard method");
                TempData["Error"] = "Failed to load dashboard.";
                return View();
            }
        }

        // GET
        public IActionResult SubmitLeave()
        {
            return View();
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> SubmitLeave(LeaveRequestDto dto)
        {
            try
            {
                var token = HttpContext.Session.GetString("JWToken");
                var empId = int.Parse(HttpContext.Session.GetString("EmpId"));
                dto.EmpId = empId;

                var success = await _employeeService.SubmitLeave(dto, token);

                if (success)
                {
                    TempData["Success"] = "Leave request submitted successfully!";
                    return RedirectToAction("Dashboard");
                }

                TempData["Error"] = "Failed to submit leave request.";
                return View(dto);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EmployeeController SubmitLeave POST method");
                TempData["Error"] = "Something went wrong while submitting leave.";
                return View(dto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CancelLeave(int requestId)
        {
            try
            {
                var token = HttpContext.Session.GetString("JWToken");
                var empId = int.Parse(HttpContext.Session.GetString("EmpId"));

                var success = await _employeeService.CancelLeave(empId, requestId, token);

                if (success)
                    TempData["Success"] = "Leave cancelled successfully!";
                else
                    TempData["Error"] = "Failed to cancel leave.";
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EmployeeController CancelLeave method");
                TempData["Error"] = "Something went wrong while cancelling leave.";
            }

            return RedirectToAction("Dashboard");
        }
    }
}