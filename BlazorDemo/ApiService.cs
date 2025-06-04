using Microsoft.Extensions.Options;

namespace BlazorDemo
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;

        public ApiService(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
        }

        public async Task<string> CallApiAsync(string serviceName)
        {
            if (_apiSettings.Endpoints.TryGetValue(serviceName, out string url))
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            return "Invalid API service name.";
        }
    }
}
