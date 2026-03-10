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
    public class TeamCalendarRepository : ITeamCalendarRepository
    {
        private readonly DapperContext _context;

        public TeamCalendarRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TeamCalendar>> GetTeamCalendarAsync(int month, int year, int deptId)
        {
            try
            {
                using var conn = _context.CreateConnection();
                return await conn.QueryAsync<TeamCalendar>(
                    "USP_GetTeamLeaveCalendar",
                    new { Month = month, Year = year, DeptId = deptId },
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in TeamCalendarRepository GetTeamCalendarAsync method");
                throw;
            }
        }
    }
}