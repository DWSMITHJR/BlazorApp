using System.ComponentModel.DataAnnotations;

namespace BlazorDemo.Models
{
    public class EmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }

    public class CodeResponse
    {
        public int Code { get; set; }
    }
}
