using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florentina_Infinion_Assessment.Application.DTOs
{
    public class ApiResponseDto<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string Message { get; set; }
        public string? Error { get; set; }

        public ApiResponseDto(bool success, T? data, string message, string? error = null)
        {
            Success = success;
            Data = data;
            Message = message;
            Error = error;
        }

        public static ApiResponseDto<T> SuccessResponse(T data, string message = "Request completed successfully")
        {
            return new ApiResponseDto<T>(true, data, message);
        }

        public static ApiResponseDto<T> FailureResponse(string error, string message = "Request failed")
        {
            return new ApiResponseDto<T>(false, default, message, error);
        }
    }
}
