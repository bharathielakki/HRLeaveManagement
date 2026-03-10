using HRLeaveManagement.MVC.Models;
using Newtonsoft.Json;
using System.Text;
using Serilog;

namespace HRLeaveManagement.MVC.Services
{
    public class AuthService
    {
        private readonly IHttpClientFactory _clientFactory;

        public AuthService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<LoginResponse> Login(string username, string password)
        {
            try
            {
                var client = _clientFactory.CreateClient("API");

                var data = new
                {
                    Username = username,
                    Password = password
                };

                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/Auth/login", content);

                if (!response.IsSuccessStatusCode)
                {
                    Log.Warning("Login failed for user {Username}. StatusCode: {StatusCode}", username, response.StatusCode);
                    return null;
                }

                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LoginResponse>(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in AuthService.Login for user {Username}", username);
                return null;
            }
        }
    }
}