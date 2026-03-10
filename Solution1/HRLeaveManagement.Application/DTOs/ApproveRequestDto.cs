using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.DTOs
{
    public class ApproveRequestDto
    {
        public int RequestId { get; set; }
        public int ManagerId { get; set; }
        public int empId { get; set; }
        public int leaveTypeId { get; set; }
        public int businessDays { get; set; }

        public string Comment { get; set; }
    }
}
