using HRLeaveManagement.Domain.Entities;
using HRLeaveManagement.Infrastructure.Data;
using HRLeaveManagement.Domain.Interfaces;
using Dapper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HRLeaveManagement.Infrastructure.Repositories
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly DapperContext _context;

        public LeaveRequestRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> SubmitLeaveRequestAsync(LeaveRequest request)
        {
            try
            {
                using var conn = _context.CreateConnection();
                return await conn.ExecuteScalarAsync<int>(
                    "USP_SubmitLeaveRequest",
                    new { request.EmpId, request.LeaveTypeId, request.StartDate, request.EndDate, request.BusinessDays },
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in LeaveRequestRepository SubmitLeaveRequestAsync method");
                throw;
            }
        }

        public async Task<IEnumerable<LeaveRequest>> GetEmployeeRequestsAsync(int empId)
        {
            try
            {
                using var conn = _context.CreateConnection();
                return await conn.QueryAsync<LeaveRequest>(
                    "USP_GetEmployeeLeaveRequests",
                    new { EmpId = empId },
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in LeaveRequestRepository GetEmployeeRequestsAsync method");
                throw;
            }
        }

        public async Task CancelLeaveRequestAsync(int requestId, int empId)
        {
            try
            {
                using var conn = _context.CreateConnection();
                await conn.ExecuteAsync(
                    "USP_CancelLeaveRequest",
                    new { RequestId = requestId, EmpId = empId },
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in LeaveRequestRepository CancelLeaveRequestAsync method");
                throw;
            }
        }

        public async Task<LeaveRequest> GetByIdAsync(int requestId)
        {
            try
            {
                using var conn = _context.CreateConnection();
                return await conn.QueryFirstOrDefaultAsync<LeaveRequest>(
                    "SELECT * FROM LeaveRequests WHERE RequestId = @RequestId",
                    new { RequestId = requestId }
                );
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in LeaveRequestRepository GetByIdAsync method");
                throw;
            }
        }

        public async Task<bool> CheckOverlapAsync(int empId, DateTime startDate, DateTime endDate)
        {
            try
            {
                using var conn = _context.CreateConnection();
                var result = await conn.QueryFirstOrDefaultAsync<int>(
                    "USP_CheckLeaveOverlap",
                    new { EmpId = empId, StartDate = startDate, EndDate = endDate },
                    commandType: CommandType.StoredProcedure
                );
                return result == 1;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in LeaveRequestRepository CheckOverlapAsync method");
                throw;
            }
        }
    }
}