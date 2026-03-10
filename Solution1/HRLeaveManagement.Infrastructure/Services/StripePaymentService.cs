using Dapper;
using HRLeaveManagement.Domain.Entities;
using HRLeaveManagement.Domain.Interfaces;
using HRLeaveManagement.Infrastructure.Data;
using Serilog;
using Stripe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HRLeaveManagement.Infrastructure.Services
{
    public class StripePaymentService : IPaymentService
    {
        private readonly DapperContext _context;

        public StripePaymentService(DapperContext context)
        {
            _context = context;
        }

        public async Task<Payment> ProcessUnpaidLeaveAsync(int requestId, int empId, decimal amount)
        {
            try
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(amount * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" },
                    Description = $"Unpaid Leave Deduction Request {requestId}"
                };

                var service = new PaymentIntentService();
                var paymentIntent = await service.CreateAsync(options);

                var payment = new Payment
                {
                    RequestId = requestId,
                    EmpId = empId,
                    Amount = amount,
                    Gateway = "Stripe",
                    GatewayRef = paymentIntent.Id,
                    Status = paymentIntent.Status,
                    PaidAt = DateTime.Now
                };

                using var connection = _context.CreateConnection();

                var id = await connection.ExecuteScalarAsync<int>(
                    @"INSERT INTO Payments(RequestId,EmpId,Amount,Gateway,GatewayRef,Status,PaidAt)
                      VALUES(@RequestId,@EmpId,@Amount,@Gateway,@GatewayRef,@Status,@PaidAt);
                      SELECT SCOPE_IDENTITY();",
                    payment
                );

                payment.PaymentId = id;
                return payment;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in StripePaymentService ProcessUnpaidLeaveAsync method");
                throw;
            }
        }

        public async Task<IEnumerable<Payment>> GetEmployeePaymentsAsync(int empId)
        {
            try
            {
                using var conn = _context.CreateConnection();
                return await conn.QueryAsync<Payment>(
                    "USP_GetEmployeePayments",
                    new { EmpId = empId },
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in StripePaymentService GetEmployeePaymentsAsync method");
                throw;
            }
        }
    }
}