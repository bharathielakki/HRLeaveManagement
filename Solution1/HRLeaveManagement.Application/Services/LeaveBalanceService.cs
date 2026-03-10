using HRLeaveManagement.Domain.Entities;
using HRLeaveManagement.Domain.Interfaces;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Services
{
    public class LeaveBalanceService
    {
        private readonly ILeaveBalanceRepository _repository;

        public LeaveBalanceService(ILeaveBalanceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LeaveBalance>> GetMyLeaveBalanceAsync(int empId, int year)
        {
            try
            {
                return await _repository.GetEmployeeLeaveBalanceAsync(empId, year);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in LeaveBalanceService GetMyLeaveBalanceAsync method");
                throw;
            }
        }
    }
}