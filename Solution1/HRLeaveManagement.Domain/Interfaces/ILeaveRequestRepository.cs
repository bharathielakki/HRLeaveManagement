using HRLeaveManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Domain.Interfaces
{
    public interface ILeaveRequestRepository
    {
        Task<int> SubmitLeaveRequestAsync(LeaveRequest request);
        Task<IEnumerable<LeaveRequest>> GetEmployeeRequestsAsync(int empId);
        Task CancelLeaveRequestAsync(int requestId, int empId);
        Task<LeaveRequest> GetByIdAsync(int requestId);
        Task<bool>CheckOverlapAsync(int empId, DateTime startDate, DateTime endDate);
    }
}
