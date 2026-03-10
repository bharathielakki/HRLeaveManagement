using HRLeaveManagement.MVC.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Serilog;

namespace HRLeaveManagement.MVC.Services
{
    public class EmployeeService
    {
        private readonly IHttpClientFactory _clientFactory;

        public EmployeeService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<LeaveRequestDto>> GetMyRequests(int empId, string token)
        {
            try
            {
                var client = _clientFactory.CreateClient("API");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"api/LeaveRequest/myrequests/{empId}");
                if (!response.IsSuccessStatusCode)
                {
                    Log.Warning("Failed to fetch requests for EmpId {EmpId}. StatusCode: {StatusCode}", empId, response.StatusCode);
                    return new List<LeaveRequestDto>();
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<LeaveRequestDto>>(json);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching requests for EmpId {EmpId}", empId);
                return new List<LeaveRequestDto>();
            }
        }

        public async Task<List<LeaveBalanceDto>> GetMyBalance(int empId, string token)
        {
            try
            {
                int year = DateTime.Now.Year;
                var client = _clientFactory.CreateClient("API");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"api/LeaveBalance/mybalance?empId={empId}&year={year}");
                if (!response.IsSuccessStatusCode)
                {
                    Log.Warning("Failed to fetch leave balance for EmpId {EmpId}. StatusCode: {StatusCode}", empId, response.StatusCode);
                    return new List<LeaveBalanceDto>();
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<LeaveBalanceDto>>(json);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching leave balance for EmpId {EmpId}", empId);
                return new List<LeaveBalanceDto>();
            }
        }

        public async Task<bool> SubmitLeave(LeaveRequestDto dto, string token)
        {
            try
            {
                var client = _clientFactory.CreateClient("API");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var json = JsonConvert.SerializeObject(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/LeaveRequest", content);
                if (!response.IsSuccessStatusCode)
                    Log.Warning("Failed to submit leave for EmpId {EmpId}. StatusCode: {StatusCode}", dto.EmpId, response.StatusCode);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error submitting leave for EmpId {EmpId}", dto.EmpId);
                return false;
            }
        }

        public async Task<bool> CancelLeave(int empId, int requestId, string token)
        {
            try
            {
                var client = _clientFactory.CreateClient("API");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.PostAsync($"/api/LeaveRequest/cancel/{empId}/{requestId}", null);
                if (!response.IsSuccessStatusCode)
                    Log.Warning("Failed to cancel leave RequestId {RequestId} for EmpId {EmpId}. StatusCode: {StatusCode}", requestId, empId, response.StatusCode);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error cancelling leave RequestId {RequestId} for EmpId {EmpId}", requestId, empId);
                return false;
            }
        }
    }
}