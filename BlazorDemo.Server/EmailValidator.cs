using System;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace BlazorDemo
{

public class EmailValidator
{
    public static int GenerateUniqueCode(string email)
    {
        if (!IsValidEmail(email))
        {
            throw new ArgumentException("Invalid email address.");
        }

        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(email));
            int code = BitConverter.ToInt32(hashBytes, 0) % 1000000;
            return Math.Abs(code);
        }
    }

    private static bool IsValidEmail(string email)
    {
        string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailPattern);
    }
}
}