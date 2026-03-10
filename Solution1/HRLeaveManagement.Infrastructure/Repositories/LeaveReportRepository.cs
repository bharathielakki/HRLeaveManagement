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
    public class LeaveReportRepository : ILeaveReportRepository
    {
        private readonly DapperContext _context;

        public LeaveReportRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LeaveReport>> GetLeaveReportAsync(LeaveReportFilter filter)
        {
            try
            {
                using var conn = _context.CreateConnection();

                return await conn.QueryAsync<LeaveReport>(
                    "USP_GetLeaveReport",
                    new
                    {
                        filter.DeptId,
                        filter.EmpId,
                        filter.StartDate,
                        filter.EndDate,
                        filter.Status
                    },
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in LeaveReportRepository GetLeaveReportAsync method");
                throw;
            }
        }
    }
}