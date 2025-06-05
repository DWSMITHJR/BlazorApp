namespace BlazorDemo
{
    /// <summary>
    /// Configuration settings for API endpoints.
    /// </summary>
    public class ApiSettings
    {
        /// <summary>
        /// Gets or sets the dictionary of API endpoints.
        /// </summary>
        public Dictionary<string, string> Endpoints { get; set; } = new Dictionary<string, string>();
    }
}
