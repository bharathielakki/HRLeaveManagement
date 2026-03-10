using HRLeaveManagement.Application.DTOs;
using HRLeaveManagement.Domain.Entities;
using HRLeaveManagement.Domain.Interfaces;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Services
{
    public class LeaveReportService
    {
        private readonly ILeaveReportRepository _repo;

        public LeaveReportService(ILeaveReportRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<LeaveReportDto>> GetLeaveReportAsync(LeaveReportFilterDto dto)
        {
            try
            {
                // Map DTO → Entity
                var filter = new LeaveReportFilter
                {
                    DeptId = dto.DeptId,
                    EmpId = dto.EmpId,
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                    Status = dto.Status
                };

                // Get data from repository
                var entities = await _repo.GetLeaveReportAsync(filter);

                // Map Entity → DTO
                var dtos = entities.Select(e => new LeaveReportDto
                {
                    RequestId = e.RequestId,
                    FullName = e.FullName,
                    Department = e.Department,
                    LeaveType = e.LeaveType,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    BusinessDays = e.BusinessDays,
                    Status = e.Status,
                    CreatedAt = e.CreatedAt
                });

                return dtos;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in LeaveReportService GetLeaveReportAsync method");
                throw;
            }
        }
    }
}