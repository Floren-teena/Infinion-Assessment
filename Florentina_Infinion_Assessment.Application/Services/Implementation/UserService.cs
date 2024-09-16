using Florentina_Infinion_Assessment.Application.DTOs;
using Florentina_Infinion_Assessment.Application.Helpers.Interfaces;
using Florentina_Infinion_Assessment.Application.Services.Interfaces;
using Florentina_Infinion_Assessment.Core.Models;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Security.Claims;

namespace Florentina_Infinion_Assessment.Application.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public UserService(UserManager<AppUser> userManager, ITokenService tokenService, IEmailService emailService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        public async Task<ApiResponseDto<UserResponseDto>> LoginUserAsync(LoginRequestDto request)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email!);
                if (user == null)
                {
                    return ApiResponseDto<UserResponseDto>.FailureResponse("Email address does not exist yet, try registering with it", "Login failed.");
                }

                var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password!);
                if (!isPasswordValid)
                {
                    return ApiResponseDto<UserResponseDto>.FailureResponse("Incorrect Password", "Login failed.");
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Email!)
                };

                var token = _tokenService.GenerateToken(claims);

                var userResponse = new UserResponseDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Token = token
                };

                return ApiResponseDto<UserResponseDto>.SuccessResponse(userResponse, "Login successful.");
            }
            catch (Exception)
            {
                return ApiResponseDto<UserResponseDto>.FailureResponse("An unexpected error occurred. Please try again later.", "Login failed.");
            }
        }

        public async Task<ApiResponseDto<UserResponseDto>> RegisterUserAsync(RegisterRequestDto request)
        {
            try
            {
                var email = new MailAddress(request.Email!);
            }
            catch (FormatException)
            {
                return ApiResponseDto<UserResponseDto>.FailureResponse("Invalid email format", "User registration failed.");
            }
            try
            {
                var existingUser = await _userManager.FindByEmailAsync(request.Email!);
                if (existingUser != null)
                {
                    return ApiResponseDto<UserResponseDto>.FailureResponse("Email already in use", "User registration failed.");
                }

                var user = new AppUser
                {
                    UserName = request.Email,
                    Email = request.Email,
                    FirstName = request.FirstName!,
                    LastName = request.LastName!
                };

                var result = await _userManager.CreateAsync(user, request.Password!);

                var emailContent = new EmailDto
                {
                    To = request.Email,
                    Subject = "Welcome to Infinion Products – Your Journey Begins Here!",
                    FirstName = request.FirstName
                };
                await _emailService.RegistrationEmailAsync(emailContent);

                if (result.Succeeded)
                {
                    var userResponse = new UserResponseDto
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Password = user.PasswordHash
                    };

                    return ApiResponseDto<UserResponseDto>.SuccessResponse(userResponse, "User registration successful.");
                }

                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return ApiResponseDto<UserResponseDto>.FailureResponse(errors, "User registration failed.");
            }
            catch (Exception)
            {
                
                return ApiResponseDto<UserResponseDto>.FailureResponse("An unexpected error occurred. Please try again later.", "User registration failed.");
            }
        }


    }
}
