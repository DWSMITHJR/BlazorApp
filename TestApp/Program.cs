using System;
using BlazorDemo;
using BlazorDemo.Server.Controllers;
using BlazorDemo.Server.Models;

Console.WriteLine("Testing EmailValidator and CodeController");
Console.WriteLine("----------------------------------------");

// Test EmailValidator
TestEmailValidator();

// Test CodeController
TestCodeController();

void TestEmailValidator()
{
    Console.WriteLine("\nTesting EmailValidator...");
    
    // Test valid email
    string validEmail = "test@example.com";
    try
    {
        int code = EmailValidator.GenerateUniqueCode(validEmail);
        Console.WriteLine($"✓ Valid email '{validEmail}' generated code: {code}");
        
        // Verify same email generates same code
        int sameCode = EmailValidator.GenerateUniqueCode(validEmail);
        Console.WriteLine($"  - Same email generates same code: {code == sameCode}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"✗ Error with valid email: {ex.Message}");
    }

    // Test invalid email
    string invalidEmail = "invalid-email";
    try
    {
        int code = EmailValidator.GenerateUniqueCode(invalidEmail);
        Console.WriteLine($"✗ Invalid email '{invalidEmail}' should not generate a code but got: {code}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"✓ Invalid email '{invalidEmail}' correctly threw: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"✗ Unexpected error with invalid email: {ex.Message}");
    }
}

void TestCodeController()
{
    Console.WriteLine("\nTesting CodeController...");
    var controller = new CodeController();

    // Test valid email
    TestValidEmail(controller, "user@example.com");
    
    // Test null email
    TestInvalidEmail(controller, null, "Null email");
    
    // Test empty email
    TestInvalidEmail(controller, "", "Empty email");
    
    // Test invalid email format
    TestInvalidEmail(controller, "invalid-email", "Invalid email format");
}

void TestValidEmail(CodeController controller, string email)
{
    try
    {
        var result = controller.GenerateCode(new EmailRequest { Email = email }) as Microsoft.AspNetCore.Mvc.OkObjectResult;
        if (result != null)
        {
            var code = result.Value?.GetType().GetProperty("Code")?.GetValue(result.Value);
            Console.WriteLine($"✓ Valid request with email '{email}' generated code: {code}");
        }
        else
        {
            Console.WriteLine($"✗ Valid request with email '{email}' failed");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"✗ Error with valid email '{email}': {ex.Message}");
    }
}

void TestInvalidEmail(CodeController controller, string email, string testCase)
{
    try
    {
        var result = controller.GenerateCode(new EmailRequest { Email = email });
        var badRequest = result as Microsoft.AspNetCore.Mvc.BadRequestObjectResult;
        
        if (badRequest != null)
        {
            Console.WriteLine($"✓ {testCase} correctly returned BadRequest: {badRequest.Value}");
        }
        else
        {
            Console.WriteLine($"✗ {testCase} did not return expected BadRequest");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"✗ Error with {testCase}: {ex.Message}");
    }
}
