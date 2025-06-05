using System;
using System.Security.Cryptography;
using System.Text;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Testing EmailValidator Implementation");
        Console.WriteLine("==================================");

        // Test valid email
        TestValidEmail("test@example.com");
        
        // Test same email generates same code
        TestSameEmailSameCode("user@example.com");
        
        // Test different emails generate different codes
        TestDifferentEmailsDifferentCodes("user1@example.com", "user2@example.com");
        
        // Test invalid email format
        TestInvalidEmail("invalid-email");
        TestInvalidEmail("@missing-username.com");
        TestInvalidEmail("missing-at.com");
    }
    
    public static void TestValidEmail(string email)
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
    
    public static void TestSameEmailSameCode(string email)
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
    
    public static void TestDifferentEmailsDifferentCodes(string email1, string email2)
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
    
    public static void TestInvalidEmail(string email)
    {
        try
        {
            int code = GenerateUniqueCode(email);
            Console.WriteLine($"✗ Invalid email '{email}' should have thrown an exception but returned code: {code}");
        }
        catch (ArgumentException)
        {
            Console.WriteLine($"✓ Invalid email '{email}' correctly threw an exception");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Unexpected error with invalid email '{email}': {ex.GetType().Name} - {ex.Message}");
        }
    }
    
    // This is the same implementation as in the original EmailValidator
    public static int GenerateUniqueCode(string email)
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
    private static bool IsValidEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;
            
        string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);
    }
}
