using Florentina_Infinion_Assessment.Application.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florentina_Infinion_Assessment.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponseDto<UserResponseDto>> RegisterUserAsync(RegisterRequestDto request);
        Task<ApiResponseDto<UserResponseDto>> LoginUserAsync(LoginRequestDto request);
    }
}
