using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Domain.Entities
{
    public class LeaveRequest
    {
        public int RequestId { get; set; }
        public int EmpId { get; set; }
        public string EmployeeName { get; set; }
        public int LeaveTypeId { get; set; }

        public string LeaveTypeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int BusinessDays { get; set; }
        public string Status { get; set; }
        public string ManagerComment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public int DeptId { get; set; }
    }
}
