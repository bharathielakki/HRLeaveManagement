using HRLeaveManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Domain.Interfaces
{
    public interface ILeaveBalanceRepository
    {
        Task<IEnumerable<LeaveBalance>> GetEmployeeLeaveBalanceAsync(int empId, int year);
        
    }
}
