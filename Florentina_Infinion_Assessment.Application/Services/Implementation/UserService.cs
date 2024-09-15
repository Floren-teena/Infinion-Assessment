using Florentina_Infinion_Assessment.Application.DTOs;
using Florentina_Infinion_Assessment.Application.Helpers.Interfaces;
using Florentina_Infinion_Assessment.Application.Services.Interfaces;
using Florentina_Infinion_Assessment.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Florentina_Infinion_Assessment.Application.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
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

                // Verify the password
                var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password!);
                if (!isPasswordValid)
                {
                    return ApiResponseDto<UserResponseDto>.FailureResponse("Incorrect Password", "Login failed.");
                }

                // If password is correct, generate a token 
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Email!)
                };

                var token = _tokenService.GenerateToken(claims);

                // Return the user details and token in a response object
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
                // Check if the email is already registered
                var existingUser = await _userManager.FindByEmailAsync(request.Email!);
                if (existingUser != null)
                {
                    return ApiResponseDto<UserResponseDto>.FailureResponse("Email already in use", "User registration failed.");
                }

                // Create a new user object
                var user = new AppUser
                {
                    UserName = request.Email,
                    Email = request.Email,
                    FirstName = request.FirstName!,
                    LastName = request.LastName!
                };

                // Attempt to create the user
                var result = await _userManager.CreateAsync(user, request.Password!);

                if (result.Succeeded)
                {
                    // If successful, return the user details wrapped in the UserResponse DTO
                    var userResponse = new UserResponseDto
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Password = user.PasswordHash
                    };

                    // Return a success response with the user details
                    return ApiResponseDto<UserResponseDto>.SuccessResponse(userResponse, "User registration successful.");
                }

                // If registration fails, return a failure response with error details
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
