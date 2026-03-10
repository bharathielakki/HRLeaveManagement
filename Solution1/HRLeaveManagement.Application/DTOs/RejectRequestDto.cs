using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.DTOs
{
    public class RejectRequestDto
    {
        public int RequestId { get; set; }
        public int ManagerId { get; set; }
        public string Reason { get; set; }
    }
}
