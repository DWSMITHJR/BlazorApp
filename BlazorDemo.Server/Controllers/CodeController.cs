using Microsoft.AspNetCore.Mvc;
using BlazorDemo;
using System;

namespace BlazorDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CodeController : ControllerBase
    {
        [HttpPost]
        [Route("generate")]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult GenerateCode([FromBody] EmailRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Email))
                {
                    return BadRequest("Email is required.");
                }

                int code = EmailValidator.GenerateUniqueCode(request.Email);
                return Ok(new { Code = code });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while generating the code.");
            }
        }
    }

    public class EmailRequest
    {
        public string Email { get; set; }
    }
}
