namespace BlazorDemo.Models
{
    public class ApiSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
        
        public ApiSettings()
        {
            // Default constructor
        }
        
        public ApiSettings(string baseUrl)
        {
            BaseUrl = baseUrl;
        }
    }
}
