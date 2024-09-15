using Florentina_Infinion_Assessment.Application.DTOs;
using Florentina_Infinion_Assessment.Application.Helpers.Interfaces;
using Florentina_Infinion_Assessment.Application.Services.Implementation;
using Florentina_Infinion_Assessment.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Florentina_Infinion_Assessment.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase 
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public UserController(ITokenService tokenService, IUserService userService, IEmailService emailService)
        {
            _tokenService = tokenService;
            _userService = userService;
            _emailService = emailService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponseDto<string>(false, null, "Invalid data", ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage));
            }

            var result = await _userService.RegisterUserAsync(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            var emailBody = $"Dear {request.FirstName},<br/><br/>" +
                            $"Thank you for registering with Infinion Products Application.<br/><br/>" +
                            $"We are committed to providing you with amazing products.<br/><br/>" + 
                            $"Feel free to browse through our wide range of products tailored to meet your needs.<br/><br/>" +
                            $"Thank you for choosing Infinion Products. We look forward to helping you find the best products!" +
                            $"Best regards,<br/><br/>" +
                            $"The Infinion Team";

            var emailContent = new EmailDto
            {
                To = request.Email,
                Subject = "Welcome to Infinion Products – Your Journey Begins Here!",
                Body = emailBody
            };

            await _emailService.RegistrationEmailAsync(emailContent);

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponseDto<string>(false, null, "Invalid data", ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage));
            }

            var result = await _userService.LoginUserAsync(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
