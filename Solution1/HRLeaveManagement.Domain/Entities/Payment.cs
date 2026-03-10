using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Domain.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int RequestId { get; set; }
        public int EmpId { get; set; }
        public decimal Amount { get; set; }
        public string Gateway { get; set; }
        public string GatewayRef { get; set; }
        public string Status { get; set; }
        public DateTime? PaidAt { get; set; }
    }
}
