using HRLeaveManagement.Application.DTOs;
using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRLeaveManagement.MVC.Controllers
{
    public class HRController : Controller
    {
        private readonly HRService _service;

        public HRController(HRService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Employees()
        {
            try
            {
                var token = HttpContext.Session.GetString("JWToken");
                var employees = await _service.GetEmployees(token);
                return View(employees);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in HRController Employees method");
                TempData["Error"] = "Failed to load employees";
                return View(new List<EmployeeDto>());
            }
        }

        public async Task<IActionResult> AddEmployee()
        {
            try
            {
                var token = HttpContext.Session.GetString("JWToken");
                ViewBag.Departments = await _service.GetDepartments(token);
                ViewBag.Managers = await _service.GetManagers(token);
                return View();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in HRController AddEmployee GET method");
                TempData["Error"] = "Failed to load form";
                return RedirectToAction("Employees");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeDto dto)
        {
            try
            {
                var token = HttpContext.Session.GetString("JWToken");
                var success = await _service.AddEmployee(dto, token);

                if (success)
                {
                    TempData["Success"] = "Employee added successfully";
                    return RedirectToAction("Employees");
                }

                TempData["Error"] = "Failed to add employee";
                return View(dto);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in HRController AddEmployee POST method");
                TempData["Error"] = "Something went wrong while adding employee";
                return View(dto);
            }
        }

        public async Task<IActionResult> EditEmployee(int id)
        {
            try
            {
                var token = HttpContext.Session.GetString("JWToken");
                var emp = await _service.GetEmployeeById(id, token);

                ViewBag.Departments = await _service.GetDepartments(token);
                ViewBag.Managers = await _service.GetManagers(token);

                return View(emp);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in HRController EditEmployee GET method");
                TempData["Error"] = "Failed to load employee";
                return RedirectToAction("Employees");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(EmployeeDto dto)
        {
            try
            {
                var token = HttpContext.Session.GetString("JWToken");
                var success = await _service.UpdateEmployee(dto, token);

                if (success)
                {
                    TempData["Success"] = "Employee updated successfully";
                    return RedirectToAction("Employees");
                }

                TempData["Error"] = "Failed to update employee";
                return View(dto);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in HRController EditEmployee POST method");
                TempData["Error"] = "Something went wrong while updating employee";
                return View(dto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Deactivate(int id)
        {
            try
            {
                var token = HttpContext.Session.GetString("JWToken");
                var success = await _service.DeactivateEmployee(id, token);

                if (success)
                    TempData["Success"] = "Employee deactivated successfully";
                else
                    TempData["Error"] = "Failed to deactivate employee";
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in HRController Deactivate method");
                TempData["Error"] = "Something went wrong while deactivating employee";
            }

            return RedirectToAction("Employees");
        }

        public async Task<IActionResult> LeaveReport()
        {
            try
            {
                var token = HttpContext.Session.GetString("JWToken");

                ViewBag.Departments = await _service.GetDepartments(token);
                ViewBag.Employees = await _service.GetEmployees(token);

                return View(new List<LeaveReportDto>());
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in HRController LeaveReport GET method");
                TempData["Error"] = "Failed to load leave report";
                return View(new List<LeaveReportDto>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> LeaveReport(LeaveReportFilterDto filter)
        {
            try
            {
                var token = HttpContext.Session.GetString("JWToken");

                var report = await _service.GetLeaveReport(filter, token);

                ViewBag.Departments = await _service.GetDepartments(token);
                ViewBag.Employees = await _service.GetEmployees(token);

                return View(report);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in HRController LeaveReport POST method");
                TempData["Error"] = "Something went wrong while generating leave report";
                return View(new List<LeaveReportDto>());
            }
        }
    }
}