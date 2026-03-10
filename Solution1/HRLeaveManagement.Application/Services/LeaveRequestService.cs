using HRLeaveManagement.Application.DTOs;
using HRLeaveManagement.Domain.Entities;
using HRLeaveManagement.Domain.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Services
{
    public class LeaveRequestService
    {
        private readonly ILeaveRequestRepository _repo;

        public LeaveRequestService(ILeaveRequestRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> SubmitAsync(LeaveRequestDto dto)
        {
            try
            {
                var request = new LeaveRequest
                {
                    EmpId = dto.EmpId,
                    LeaveTypeId = dto.LeaveTypeId,
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                    BusinessDays = dto.BusinessDays,
                    Status = "Pending",
                    CreatedAt = DateTime.Now
                };

                bool isOverlap = await _repo.CheckOverlapAsync(dto.EmpId, dto.StartDate, dto.EndDate);

                if (isOverlap)
                    throw new Exception("You already have an approved leave overlapping with the selected dates.");

                return await _repo.SubmitLeaveRequestAsync(request);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in LeaveRequestService SubmitAsync method");
                throw;
            }
        }

        public async Task<IEnumerable<LeaveRequest>> GetMyRequestsAsync(int empId)
        {
            try
            {
                return await _repo.GetEmployeeRequestsAsync(empId);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in LeaveRequestService GetMyRequestsAsync method");
                throw;
            }
        }

        public async Task CancelAsync(int requestId, int empId)
        {
            try
            {
                var request = await _repo.GetByIdAsync(requestId);
                if (request.Status != "Pending")
                    throw new Exception("Cannot cancel processed request");

                await _repo.CancelLeaveRequestAsync(requestId, empId);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in LeaveRequestService CancelAsync method");
                throw;
            }
        }
    }
}