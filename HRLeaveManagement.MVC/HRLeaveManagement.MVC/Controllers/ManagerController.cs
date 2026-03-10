using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;

namespace HRLeaveManagement.MVC.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ManagerService _managerService;

        public ManagerController(ManagerService managerService)
        {
            _managerService = managerService;
        }

        // Manager Dashboard
        public async Task<IActionResult> Dashboard()
        {
            try
            {
                var token = HttpContext.Session.GetString("JWToken");
                var requests = await _managerService.GetPendingRequests(token);
                return View(requests);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in ManagerController Dashboard method");
                TempData["Error"] = "Failed to load pending requests";
                return View(new object[0]);
            }
        }

        // Approve
        [HttpPost]
        public async Task<IActionResult> Approve(int requestId, string comment, int EmpId, int LeaveTypeId, int BusinessDays)
        {
            try
            {
                var token = HttpContext.Session.GetString("JWToken");
                var managerId = int.Parse(HttpContext.Session.GetString("EmpId"));

                var dto = new ApproveRequestDto
                {
                    RequestId = requestId,
                    ManagerId = managerId,
                    empId = EmpId,
                    leaveTypeId = LeaveTypeId,
                    businessDays = BusinessDays,
                    Comment = comment
                };

                await _managerService.ApproveLeave(dto, token);
                TempData["Success"] = "Leave approved successfully!";
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in ManagerController Approve method");
                TempData["Error"] = "Failed to approve leave";
            }

            return RedirectToAction("Dashboard");
        }

        // Reject
        [HttpPost]
        public async Task<IActionResult> Reject(int requestId, string comment)
        {
            try
            {
                var token = HttpContext.Session.GetString("JWToken");
                var managerId = int.Parse(HttpContext.Session.GetString("EmpId"));

                var dto = new RejectRequestDto
                {
                    RequestId = requestId,
                    ManagerId = managerId,
                    Reason = comment
                };

                await _managerService.RejectLeave(dto, token);
                TempData["Success"] = "Leave rejected successfully!";
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in ManagerController Reject method");
                TempData["Error"] = "Failed to reject leave";
            }

            return RedirectToAction("Dashboard");
        }

     
        
    }
}