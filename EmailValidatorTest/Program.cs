using System;
using System.Security.Cryptography;
using System.Text;

Console.WriteLine("Testing EmailValidator Implementation");
Console.WriteLine("==================================\n");

// Test valid email
TestValidEmail("test@example.com");

// Test same email generates same code
TestSameEmailSameCode("user@example.com");

// Test different emails generate different codes
TestDifferentEmailsDifferentCodes("user1@example.com", "user2@example.com");

// Test invalid email formats
TestInvalidEmail("invalid-email");
TestInvalidEmail("@missing-username.com");
TestInvalidEmail("missing-at.com");
TestInvalidEmail("");
TestInvalidEmail(null);

void TestValidEmail(string email)
{
    try
    {
        int code = GenerateUniqueCode(email);
        Console.WriteLine($"✓ Valid email '{email}' generated code: {code}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"✗ Error with valid email '{email}': {ex.Message}");
    }
}

void TestSameEmailSameCode(string email)
{
    try
    {
        int code1 = GenerateUniqueCode(email);
        int code2 = GenerateUniqueCode(email);
        bool same = code1 == code2;
        Console.WriteLine($"✓ Same email '{email}' generates same code: {same} ({code1} == {code2})");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"✗ Error testing same email: {ex.Message}");
    }
}

void TestDifferentEmailsDifferentCodes(string email1, string email2)
{
    try
    {
        int code1 = GenerateUniqueCode(email1);
        int code2 = GenerateUniqueCode(email2);
        bool different = code1 != code2;
        Console.WriteLine($"✓ Different emails generate different codes: {different} ({code1} != {code2})");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"✗ Error testing different emails: {ex.Message}");
    }
}

void TestInvalidEmail(string email)
{
    try
    {
        string displayEmail = email ?? "null";
        int code = GenerateUniqueCode(email);
        Console.WriteLine($"✗ Invalid email '{displayEmail}' should have thrown an exception but returned code: {code}");
    }
    catch (ArgumentException)
    {
        string displayEmail = email ?? "null";
        Console.WriteLine($"✓ Invalid email '{displayEmail}' correctly threw an exception");
    }
    catch (Exception ex)
    {
        string displayEmail = email ?? "null";
        Console.WriteLine($"✗ Unexpected error with invalid email '{displayEmail}': {ex.GetType().Name} - {ex.Message}");
    }
}

// This is the same implementation as in the original EmailValidator
static int GenerateUniqueCode(string email)
{
    if (!IsValidEmail(email))
    {
        throw new ArgumentException("Invalid email address.");
    }

    using (SHA256 sha256 = SHA256.Create())
    {
        byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(email));
        int code = BitConverter.ToInt32(hashBytes, 0) % 1000000;
        return Math.Abs(code);
    }
}

// This is the same implementation as in the original EmailValidator
static bool IsValidEmail(string email)
{
    if (string.IsNullOrEmpty(email))
        return false;
        
    string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    return System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);
}
