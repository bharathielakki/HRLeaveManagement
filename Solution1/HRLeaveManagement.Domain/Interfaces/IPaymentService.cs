using HRLeaveManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Domain.Interfaces
{
    public interface IPaymentService
    {
        Task<Payment> ProcessUnpaidLeaveAsync(int requestId, int empId, decimal amount);

        Task<IEnumerable<Payment>> GetEmployeePaymentsAsync(int empId);
    }
}
