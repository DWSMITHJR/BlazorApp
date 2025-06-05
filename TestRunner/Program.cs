using BlazorDemo;
using BlazorDemo.Server.Controllers;
using BlazorDemo.Server.Models;

Console.WriteLine("Testing EmailValidator...");

// Test valid email
string validEmail = "test@example.com";
try
{
    int code = EmailValidator.GenerateUniqueCode(validEmail);
    Console.WriteLine($"Valid email '{validEmail}' generated code: {code}");
    
    // Test that the same email generates the same code
    int sameCode = EmailValidator.GenerateUniqueCode(validEmail);
    Console.WriteLine($"Same email generated same code: {code == sameCode}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error with valid email: {ex.Message}");
}

// Test invalid email
string invalidEmail = "invalid-email";
try
{
    int code = EmailValidator.GenerateUniqueCode(invalidEmail);
    Console.WriteLine($"Invalid email '{invalidEmail}' generated code: {code}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error with invalid email '{invalidEmail}': {ex.Message}");
}

// Test CodeController
Console.WriteLine("\nTesting CodeController...");
var controller = new CodeController();

// Test valid email
var validRequest = new EmailRequest { Email = "user@example.com" };
try
{
    var result = controller.GenerateCode(validRequest) as Microsoft.AspNetCore.Mvc.OkObjectResult;
    if (result != null)
    {
        var code = result.Value?.GetType().GetProperty("Code")?.GetValue(result.Value);
        Console.WriteLine($"Valid request generated code: {code}");
    }
    else
    {
        Console.WriteLine("Valid request failed");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error with valid request: {ex.Message}");
}

// Test null email
try
{
    var result = controller.GenerateCode(new EmailRequest { Email = null }) as Microsoft.AspNetCore.Mvc.BadRequestObjectResult;
    Console.WriteLine($"Null email error: {result?.Value}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error with null email: {ex.Message}");
}

// Test empty email
try
{
    var result = controller.GenerateCode(new EmailRequest { Email = string.Empty }) as Microsoft.AspNetCore.Mvc.BadRequestObjectResult;
    Console.WriteLine($"Empty email error: {result?.Value}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error with empty email: {ex.Message}");
}
