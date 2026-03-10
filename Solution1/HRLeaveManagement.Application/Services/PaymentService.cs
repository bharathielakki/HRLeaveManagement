using HRLeaveManagement.Domain.Entities;
using HRLeaveManagement.Domain.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Services
{
    public class PaymentService
    {
        private readonly IPaymentService _paymentService;

        public PaymentService(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<Payment> ProcessUnpaidLeaveAsync(int requestId, int empId, decimal amount)
        {
            try
            {
                return await _paymentService.ProcessUnpaidLeaveAsync(requestId, empId, amount);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in PaymentService ProcessUnpaidLeaveAsync method");
                throw;
            }
        }

        public async Task<IEnumerable<Payment>> GetEmployeePaymentHistoryAsync(int empId)
        {
            try
            {
                return await _paymentService.GetEmployeePaymentsAsync(empId);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in PaymentService GetEmployeePaymentHistoryAsync method");
                throw;
            }
        }
    }
}