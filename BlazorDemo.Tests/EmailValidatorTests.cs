using Xunit;
using BlazorDemo;
using System;

namespace BlazorDemo.Tests
{
    public class EmailValidatorTests
    {
        [Theory]
        [InlineData("test@example.com")]
        [InlineData("user.name@domain.com")]
        [InlineData("user+tag@example.com")]
        public void GenerateUniqueCode_ValidEmail_ReturnsSixDigitCode(string email)
        {
            // Act
            var result = EmailValidator.GenerateUniqueCode(email);

            // Assert
            Assert.InRange(result, 0, 999999);
        }

        [Theory]
        [InlineData("invalid-email")]
        [InlineData("missing-at.com")]
        [InlineData("@missing-username.com")]
        [InlineData("")]
        [InlineData(null)]
        public void GenerateUniqueCode_InvalidEmail_ThrowsArgumentException(string email)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => EmailValidator.GenerateUniqueCode(email));
        }

        [Fact]
        public void GenerateUniqueCode_SameEmail_ReturnsSameCode()
        {
            // Arrange
            var email = "test@example.com";

            // Act
            var result1 = EmailValidator.GenerateUniqueCode(email);
            var result2 = EmailValidator.GenerateUniqueCode(email);

            // Assert
            Assert.Equal(result1, result2);
        }


        [Fact]
        public void GenerateUniqueCode_DifferentEmails_ReturnsDifferentCodes()
        {
            // Arrange
            var email1 = "test1@example.com";
            var email2 = "test2@example.com";

            // Act
            var result1 = EmailValidator.GenerateUniqueCode(email1);
            var result2 = EmailValidator.GenerateUniqueCode(email2);

            // Assert
            Assert.NotEqual(result1, result2);
        }
    }
}
