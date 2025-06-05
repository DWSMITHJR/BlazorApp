using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorDemo.Models;

namespace BlazorDemo.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ApiService(HttpClient httpClient, ApiSettings apiSettings)
        {
            _httpClient = httpClient;
            _baseUrl = apiSettings.BaseUrl.TrimEnd('/');
        }

        public async Task<int> GenerateCodeAsync(string email)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/code/generate", new { Email = email });
            response.EnsureSuccessStatusCode();
            
            var result = await response.Content.ReadFromJsonAsync<CodeGenerationResult>();
            return result.Code;
        }

        public async Task<string> CallApiAsync(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/{endpoint}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calling API: {ex.Message}");
                return $"Error: {ex.Message}";
            }
        }
    }

    public class CodeGenerationResult
    {
        public int Code { get; set; }
    }
}
