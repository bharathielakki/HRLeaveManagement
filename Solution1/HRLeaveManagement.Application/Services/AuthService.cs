using HRLeaveManagement.Application.DTOs;
using HRLeaveManagement.Domain.Interfaces;
using Serilog;

namespace HRLeaveManagement.Application.Services
{
    public class AuthService
    {
        private readonly IAuthRepository _repo;
        private readonly JwtService _jwtService;

        public AuthService(IAuthRepository repo, JwtService jwtService)
        {
            _repo = repo;
            _jwtService = jwtService;
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            try
            {
                var user = await _repo.GetUserByUsernameAsync(dto.Username);

                if (user == null || user.PasswordHash != dto.Password)
                    throw new Exception("Invalid username or password");

                return _jwtService.GenerateToken(user);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in AuthService LoginAsync method");
                throw;
            }
        }
    }
}