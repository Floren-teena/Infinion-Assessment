using Florentina_Infinion_Assessment.Application.DTOs;
using Florentina_Infinion_Assessment.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Florentina_Infinion_Assessment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase 
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var result = await _userService.RegisterUserAsync(request);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        { 
            var result = await _userService.LoginUserAsync(request);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
    }
}
