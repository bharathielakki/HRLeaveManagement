using Dapper;
using HRLeaveManagement.Domain.Entities;
using HRLeaveManagement.Domain.Interfaces;
using HRLeaveManagement.Infrastructure.Data;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HRLeaveManagement.Infrastructure.Repositories
{
    public class LeaveBalanceRepository : ILeaveBalanceRepository
    {
        private readonly DapperContext _context;

        public LeaveBalanceRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LeaveBalance>> GetEmployeeLeaveBalanceAsync(int empId, int year)
        {
            try
            {
                using var conn = _context.CreateConnection();

                return await conn.QueryAsync<LeaveBalance>(
                    "USP_GetEmployeeLeaveBalance",
                    new { EmpId = empId, Year = year },
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in LeaveBalanceRepository GetEmployeeLeaveBalanceAsync method");
                throw;
            }
        }
    }
}