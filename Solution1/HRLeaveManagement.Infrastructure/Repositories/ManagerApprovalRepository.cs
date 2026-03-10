using Dapper;
using HRLeaveManagement.Domain.Entities;
using HRLeaveManagement.Domain.Interfaces;
using HRLeaveManagement.Infrastructure.Data;
using HRLeaveManagement.Infrastructure.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HRLeaveManagement.Infrastructure.Repositories
{
    public class ManagerApprovalRepository : IManagerApprovalRepository
    {
        private readonly DapperContext _context;
        private readonly IPaymentService _paymentService;

        public ManagerApprovalRepository(DapperContext context, IPaymentService paymentService)
        {
            _context = context;
            _paymentService = paymentService;
        }

        public async Task<IEnumerable<LeaveRequest>> GetPendingRequestsAsync(int? deptId = null)
        {
            try
            {
                using var conn = _context.CreateConnection();
                return await conn.QueryAsync<LeaveRequest>(
                    "USP_GetPendingRequests",
                    new { DeptId = deptId },
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in ManagerApprovalRepository GetPendingRequestsAsync method");
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
                Log.Error(ex, "Error in ManagerApprovalRepository GetByIdAsync method");
                throw;
            }
        }

        public async Task ApproveAsync(int requestId, int managerId, int empId, int leaveTypeId, int businessDays, string comment)
        {
            try
            {
                if (leaveTypeId == 3) // Assume 3 = Unpaid Leave
                {
                    decimal dailySalary = 100;
                    decimal amount = businessDays * dailySalary;

                    var payment = await _paymentService.ProcessUnpaidLeaveAsync(requestId, empId, amount);

                    if (payment.Status != "requires_payment_method" && payment.Status != "succeeded")
                    {
                        throw new Exception("Payment failed");
                    }
                }

                using var conn = _context.CreateConnection();
                await conn.ExecuteAsync(
                    "USP_ApproveLeaveRequest",
                    new
                    {
                        RequestId = requestId,
                        ManagerId = managerId,
                        Comment = comment,
                        BusinessDays = businessDays,
                        EmpId = empId,
                        LeaveTypeId = leaveTypeId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in ManagerApprovalRepository ApproveAsync method");
                throw;
            }
        }

        public async Task RejectAsync(int requestId, int managerId, string comment)
        {
            try
            {
                using var conn = _context.CreateConnection();
                await conn.ExecuteAsync(
                    "USP_RejectLeaveRequest",
                    new { RequestId = requestId, ManagerId = managerId, Comment = comment },
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in ManagerApprovalRepository RejectAsync method");
                throw;
            }
        }
    }
}