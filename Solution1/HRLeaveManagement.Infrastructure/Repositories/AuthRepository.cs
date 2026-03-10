using Dapper;
using HRLeaveManagement.Domain.Entities;
using HRLeaveManagement.Domain.Interfaces;
using HRLeaveManagement.Infrastructure.Data;
using Serilog;
using System;
using System.Data;
using System.Threading.Tasks;

namespace HRLeaveManagement.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DapperContext _context;

        public AuthRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            try
            {
                using var conn = _context.CreateConnection();

                return await conn.QueryFirstOrDefaultAsync<User>(
                    "USP_LoginUser",
                    new { Username = username },
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                
                Log.Error(ex, "Error in AuthRepository GetUserByUsernameAsync");
                throw; 
            }
        }
    }
}