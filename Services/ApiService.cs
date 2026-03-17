using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;

namespace ClinicQueueFrontend.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _context;

        public ApiService(HttpClient httpClient, IHttpContextAccessor context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        private void AddToken()
        {
            var token = _context.HttpContext.Session.GetString("token");

            if (!string.IsNullOrEmpty(token))
            {
                // Clear old header
                _httpClient.DefaultRequestHeaders.Authorization = null;

                // Add new token
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<T> GetAsync<T>(string url)
        {
            AddToken();

            var res = await _httpClient.GetAsync(url);
            var result = await res.Content.ReadAsStringAsync();

            if (!res.IsSuccessStatusCode)
            {
                throw new Exception("GET API Error: " + result);
            }

            return JsonConvert.DeserializeObject<T>(result);
        }

        public async Task<T> PostAsync<T>(string url, object data)
        {
            AddToken();

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("POST API Error: " + result);
            }

            return JsonConvert.DeserializeObject<T>(result);
        }
        public async Task<T> PatchAsync<T>(string url, object data)
        {
            AddToken();

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(new HttpMethod("PATCH"), url)
            {
                Content = content
            };

            var response = await _httpClient.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("PATCH API Error: " + result);
            }

            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}