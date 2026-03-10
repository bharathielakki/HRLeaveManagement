using HRLeaveManagement.Application.DTOs;
using HRLeaveManagement.MVC.Models;
using Newtonsoft.Json;
using Serilog;
using System.Net.Http.Headers;
using System.Text;

namespace HRLeaveManagement.MVC.Services
{
    public class HRService
    {
        private readonly IHttpClientFactory _clientFactory;

        public HRService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<EmployeeDto>> GetEmployees(string token)
        {
            try
            {
                var client = _clientFactory.CreateClient("API");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync("api/Employees");

                if (!response.IsSuccessStatusCode)
                {
                    Log.Error("Error in HRService GetEmployees: StatusCode {StatusCode}", response.StatusCode);
                    return new List<EmployeeDto>();
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<EmployeeDto>>(json);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception in HRService GetEmployees method");
                return new List<EmployeeDto>();
            }
        }

        public async Task<bool> AddEmployee(EmployeeDto dto, string token)
        {
            try
            {
                var client = _clientFactory.CreateClient("API");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var content = new StringContent(
                    JsonConvert.SerializeObject(dto),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PostAsync("api/Employees", content);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Log.Error("Error in HRService AddEmployee: {Error}", error);
                }

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception in HRService AddEmployee method");
                return false;
            }
        }

        public async Task<List<DepartmentDto>> GetDepartments(string token)
        {
            try
            {
                var client = _clientFactory.CreateClient("API");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync("api/Employees/Department");

                if (!response.IsSuccessStatusCode)
                {
                    Log.Error("Error in HRService GetDepartments: StatusCode {StatusCode}", response.StatusCode);
                    return new List<DepartmentDto>();
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<DepartmentDto>>(json);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception in HRService GetDepartments method");
                return new List<DepartmentDto>();
            }
        }

        public async Task<List<EmployeeDto>> GetManagers(string token)
        {
            try
            {
                var client = _clientFactory.CreateClient("API");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync("api/Employees/managers");

                if (!response.IsSuccessStatusCode)
                {
                    Log.Error("Error in HRService GetManagers: StatusCode {StatusCode}", response.StatusCode);
                    return new List<EmployeeDto>();
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<EmployeeDto>>(json);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception in HRService GetManagers method");
                return new List<EmployeeDto>();
            }
        }

        public async Task<EmployeeDto> GetEmployeeById(int id, string token)
        {
            try
            {
                var client = _clientFactory.CreateClient("API");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"api/Employees/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    Log.Error("Error in HRService GetEmployeeById: StatusCode {StatusCode}", response.StatusCode);
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<EmployeeDto>(json);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception in HRService GetEmployeeById method");
                return null;
            }
        }

        public async Task<bool> UpdateEmployee(EmployeeDto dto, string token)
        {
            try
            {
                var client = _clientFactory.CreateClient("API");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var content = new StringContent(
                    JsonConvert.SerializeObject(dto),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PutAsync($"api/Employees/{dto.EmpId}", content);

                if (!response.IsSuccessStatusCode)
                    Log.Error("Error in HRService UpdateEmployee: StatusCode {StatusCode}", response.StatusCode);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception in HRService UpdateEmployee method");
                return false;
            }
        }

        public async Task<bool> DeactivateEmployee(int id, string token)
        {
            try
            {
                var client = _clientFactory.CreateClient("API");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var response = await client.DeleteAsync($"api/Employees/{id}");

                if (!response.IsSuccessStatusCode)
                    Log.Error("Error in HRService DeactivateEmployee: StatusCode {StatusCode}", response.StatusCode);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception in HRService DeactivateEmployee method");
                return false;
            }
        }

        public async Task<List<LeaveReportDto>> GetLeaveReport(LeaveReportFilterDto filter, string token)
        {
            try
            {
                var client = _clientFactory.CreateClient("API");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var content = new StringContent(
                    JsonConvert.SerializeObject(filter),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PostAsync("api/LeaveReport/report", content);

                if (!response.IsSuccessStatusCode)
                {
                    Log.Error("Error in HRService GetLeaveReport: StatusCode {StatusCode}", response.StatusCode);
                    return new List<LeaveReportDto>();
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<LeaveReportDto>>(json);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception in HRService GetLeaveReport method");
                return new List<LeaveReportDto>();
            }
        }
    }
}