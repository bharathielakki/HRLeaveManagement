using HRLeaveManagement.Application.DTOs;
using HRLeaveManagement.Domain.Entities;
using HRLeaveManagement.Domain.Interfaces;
using Serilog;

namespace HRLeaveManagement.Application.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeService(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            try
            {
                return await _repo.GetAllAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EmployeeService GetAllAsync method");
                throw;
            }
        }

        public async Task<Employee> GetByIdAsync(int empId)
        {
            try
            {
                return await _repo.GetByIdAsync(empId);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EmployeeService GetByIdAsync method");
                throw;
            }
        }

        public async Task<int> CreateAsync(EmployeeDto dto)
        {
            try
            {
                var employee = new Employee
                {
                    FullName = dto.FullName,
                    Email = dto.Email,
                    DeptId = dto.DeptId,
                    ManagerId = dto.ManagerId,
                    IsActive = dto.IsActive
                };

                return await _repo.CreateAsync(employee);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EmployeeService CreateAsync method");
                throw;
            }
        }

        public async Task UpdateAsync(int empId, EmployeeDto dto)
        {
            try
            {
                var employee = new Employee
                {
                    EmpId = empId,
                    FullName = dto.FullName,
                    Email = dto.Email,
                    DeptId = dto.DeptId,
                    ManagerId = dto.ManagerId,
                    IsActive = dto.IsActive
                };

                await _repo.UpdateAsync(employee);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EmployeeService UpdateAsync method");
                throw;
            }
        }

        public async Task DeleteAsync(int empId)
        {
            try
            {
                await _repo.DeleteAsync(empId);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EmployeeService DeleteAsync method");
                throw;
            }
        }

        public async Task<IEnumerable<Department>> GetDepartmentAsync()
        {
            try
            {
                return await _repo.GetDepartmentAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EmployeeService GetDepartmentAsync method");
                throw;
            }
        }

        public async Task<IEnumerable<Manager>> GetManagersAsync()
        {
            try
            {
                return await _repo.GetManagersAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EmployeeService GetManagersAsync method");
                throw;
            }
        }
    }
}