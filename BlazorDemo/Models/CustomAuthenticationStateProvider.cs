using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;
using BlazorDemo.Models;
using BlazorDemo.Services;
using System.Collections.Generic;

namespace BlazorDemo.Models
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IApiService _apiService;
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public CustomAuthenticationStateProvider(
            IApiService apiService,
            HttpClient httpClient,
            ApiSettings apiSettings)
        {
            _apiService = apiService;
            _httpClient = httpClient;
            _baseUrl = apiSettings.BaseUrl.TrimEnd('/');
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            var user = await GetUserAsync();

            if (user != null && !string.IsNullOrEmpty(user.Email))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                    new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(ClaimTypes.NameIdentifier, user.Id ?? string.Empty)
                };

                // Add phone number claim if available
                if (!string.IsNullOrEmpty(user.PhoneNumber))
                {
                    claims.Add(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
                }

                // Add additional custom claims
                if (!string.IsNullOrEmpty(user.FullName))
                {
                    claims.Add(new Claim("FullName", user.FullName));
                }

                identity = new ClaimsIdentity(claims, "CustomAuth");
            }

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        private async Task<CustomUserAccount?> GetUserAsync()
        {
            try
            {
                // Try to get the current user from the server
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/user/current");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CustomUserAccount>();
                }
                return null;
            }
            catch
            {
                // If there's an error, return null (user is not authenticated)
                return null;
            }
        }
    }
}
