using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Domain.Entities
{
    public class LeaveReport
    {
        public int RequestId { get; set; }

        public string FullName { get; set; }

        public string Department { get; set; }

        public string LeaveType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int BusinessDays { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
