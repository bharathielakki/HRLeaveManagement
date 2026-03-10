using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.DTOs
{
    public class TeamCalendarDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Employee { get; set; }
        public string LeaveType { get; set; }
        public string Status { get; set; }
    }
}
