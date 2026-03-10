using HRLeaveManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Domain.Interfaces
{
    public interface IManagerApprovalRepository
    {
        Task<IEnumerable<LeaveRequest>> GetPendingRequestsAsync(int? deptId = null);
        Task<LeaveRequest> GetByIdAsync(int requestId);
        Task ApproveAsync(int requestId, int managerId, int empId, int leaveTypeId, int businessDays, string comment );
        Task RejectAsync(int requestId, int managerId, string comment);
    }
}
