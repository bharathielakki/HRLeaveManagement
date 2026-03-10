using HRLeaveManagement.Application.DTOs;
using HRLeaveManagement.Domain.Entities;
using HRLeaveManagement.Domain.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Services
{
    public class ManagerApprovalService
    {
        private readonly IManagerApprovalRepository _repo;

        public ManagerApprovalService(IManagerApprovalRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<LeaveRequest>> GetPendingRequestsAsync(int? deptId = null)
        {
            try
            {
                return await _repo.GetPendingRequestsAsync(deptId);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in ManagerApprovalService GetPendingRequestsAsync method");
                throw;
            }
        }

        public async Task ApproveAsync(ApproveRequestDto dto)
        {
            try
            {
                await _repo.ApproveAsync(dto.RequestId, dto.ManagerId, dto.empId, dto.leaveTypeId, dto.businessDays, dto.Comment);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in ManagerApprovalService ApproveAsync method");
                throw;
            }
        }

        public async Task RejectAsync(RejectRequestDto dto)
        {
            try
            {
                await _repo.RejectAsync(dto.RequestId, dto.ManagerId, dto.Reason);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in ManagerApprovalService RejectAsync method");
                throw;
            }
        }

        public async Task<LeaveRequest> GetByIdAsync(ApproveRequestDto dto)
        {
            try
            {
                return await _repo.GetByIdAsync(dto.RequestId);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in ManagerApprovalService GetByIdAsync method");
                throw;
            }
        }
    }
}