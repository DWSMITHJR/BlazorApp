using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorDemo.Models
{
    public class CustomUserAccount
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        
        // Additional properties
        public string? FullName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? Provider { get; set; }
        public long? AccessCode { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}
