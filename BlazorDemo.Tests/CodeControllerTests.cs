using Microsoft.AspNetCore.Mvc;
using Xunit;
using BlazorDemo.Server.Controllers;
using BlazorDemo.Server.Models;
using System;

namespace BlazorDemo.Tests
{
    public class CodeControllerTests
    {
        private readonly CodeController _controller;

        public CodeControllerTests()
        {
            _controller = new CodeController();
        }

        [Fact]
        public void Generate_ValidEmail_ReturnsOkWithCode()
        {
            // Arrange
            var request = new EmailRequest { Email = "test@example.com" };

            // Act
            var result = _controller.GenerateCode(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            Assert.True(okResult.Value.GetType().GetProperty("Code") != null);
        }

        [Fact]
        public void Generate_NullEmail_ReturnsBadRequest()
        {
            // Arrange
            var request = new EmailRequest { Email = null };

            // Act
            var result = _controller.GenerateCode(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Email is required.", badRequestResult.Value);
        }

        [Fact]
        public void Generate_EmptyEmail_ReturnsBadRequest()
        {
            // Arrange
            var request = new EmailRequest { Email = string.Empty };

            // Act
            var result = _controller.GenerateCode(request);


            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Email is required.", badRequestResult.Value);
        }

        [Fact]
        public void Generate_InvalidEmail_ReturnsBadRequest()
        {
            // Arrange
            var request = new EmailRequest { Email = "invalid-email" };

            // Act
            var result = _controller.GenerateCode(request);


            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid email address.", badRequestResult.Value);
        }
    }
}
