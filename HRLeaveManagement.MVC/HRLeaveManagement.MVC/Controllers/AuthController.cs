using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HRLeaveManagement.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                var result = await _authService.Login(model.Username, model.Password);

                if (result == null)
                {
                    ViewBag.Error = "Invalid Login";
                    return View();
                }

                // Save token
                HttpContext.Session.SetString("JWToken", result.Token);

                // Decode JWT
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(result.Token);

                var role = jwtToken.Claims.First(x => x.Type == ClaimTypes.Role).Value;
                var empId = jwtToken.Claims.First(x => x.Type == "EmpId").Value;

                HttpContext.Session.SetString("Role", role);
                HttpContext.Session.SetString("EmpId", empId);

                if (role == "Employee")
                    return RedirectToAction("Dashboard", "Employee");

                if (role == "Manager")
                    return RedirectToAction("Dashboard", "Manager");

                if (role == "HRAdmin")
                    return RedirectToAction("Employees", "HR");

                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in AuthController Login method");
                ViewBag.Error = "Something went wrong, please try again.";
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Auth");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in AuthController Logout method");
                return RedirectToAction("Login");
            }
        }
    }
}