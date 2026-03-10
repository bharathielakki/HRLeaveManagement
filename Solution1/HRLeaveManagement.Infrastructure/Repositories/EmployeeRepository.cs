using Dapper;
using HRLeaveManagement.Domain.Entities;
using HRLeaveManagement.Domain.Interfaces;
using HRLeaveManagement.Infrastructure.Data;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HRLeaveManagement.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperContext _context;

        public EmployeeRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Employee employee)
        {
            try
            {
                using var conn = _context.CreateConnection();
                var id = await conn.ExecuteScalarAsync<int>(
                    "USP_AddEmployee",
                    new
                    {
                        employee.FullName,
                        employee.Email,
                        employee.DeptId,
                        employee.ManagerId
                    },
                    commandType: CommandType.StoredProcedure
                );
                return id;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EmployeeRepository CreateAsync method");
                throw;
            }
        }

        public async Task DeleteAsync(int empId)
        {
            try
            {
                using var conn = _context.CreateConnection();
                await conn.ExecuteAsync("USP_DeleteEmployee", new { EmpId = empId }, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EmployeeRepository DeleteAsync method");
                throw;
            }
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            try
            {
                using var conn = _context.CreateConnection();
                return await conn.QueryAsync<Employee>("USP_GetEmployees", commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EmployeeRepository GetAllAsync method");
                throw;
            }
        }

        public async Task<Employee> GetByIdAsync(int empId)
        {
            try
            {
                using var conn = _context.CreateConnection();
                return await conn.QueryFirstOrDefaultAsync<Employee>(
                    "USP_GetEmployeeById",
                    new { EmpId = empId },
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EmployeeRepository GetByIdAsync method");
                throw;
            }
        }

        public async Task UpdateAsync(Employee employee)
        {
            try
            {
                using var conn = _context.CreateConnection();
                await conn.ExecuteAsync(
                    "USP_UpdateEmployee",
                    new
                    {
                        employee.EmpId,
                        employee.FullName,
                        employee.Email,
                        employee.DeptId,
                        employee.ManagerId,
                        employee.IsActive
                    },
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EmployeeRepository UpdateAsync method");
                throw;
            }
        }

        public async Task<IEnumerable<Department>> GetDepartmentAsync()
        {
            try
            {
                using var conn = _context.CreateConnection();
                return await conn.QueryAsync<Department>("USP_Department", commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EmployeeRepository GetDepartmentAsync method");
                throw;
            }
        }

        public async Task<IEnumerable<Manager>> GetManagersAsync()
        {
            try
            {
                using var connection = _context.CreateConnection();
                return await connection.QueryAsync<Manager>(
                    "USP_GetManagers",
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EmployeeRepository GetManagersAsync method");
                throw;
            }
        }
    }
}