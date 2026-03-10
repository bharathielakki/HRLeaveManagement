using HRLeaveManagement.MVC.Models;
using Newtonsoft.Json;
using Serilog;
using System.Net.Http.Headers;
using System.Text;

namespace HRLeaveManagement.MVC.Services
{
    public class ManagerService
    {
        private readonly IHttpClientFactory _clientFactory;

        public ManagerService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        // Get Pending Requests
        public async Task<List<LeaveRequestDto>> GetPendingRequests(string token)
        {
            try
            {
                var client = _clientFactory.CreateClient("API");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync("api/manager/pendingrequests");

                if (!response.IsSuccessStatusCode)
                {
                    Log.Error("Error in ManagerService GetPendingRequests: StatusCode {StatusCode}", response.StatusCode);
                    return new List<LeaveRequestDto>();
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<LeaveRequestDto>>(json);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception in ManagerService GetPendingRequests method");
                return new List<LeaveRequestDto>();
            }
        }

        // Approve Leave
        public async Task<bool> ApproveLeave(ApproveRequestDto dto, string token)
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

                var response = await client.PostAsync("api/manager/approve", content);

                if (!response.IsSuccessStatusCode)
                    Log.Error("Error in ManagerService ApproveLeave: StatusCode {StatusCode}", response.StatusCode);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception in ManagerService ApproveLeave method");
                return false;
            }
        }

        // Reject Leave
        public async Task<bool> RejectLeave(RejectRequestDto dto, string token)
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

                var response = await client.PostAsync("api/manager/reject", content);

                if (!response.IsSuccessStatusCode)
                    Log.Error("Error in ManagerService RejectLeave: StatusCode {StatusCode}", response.StatusCode);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception in ManagerService RejectLeave method");
                return false;
            }
        }
    }
}