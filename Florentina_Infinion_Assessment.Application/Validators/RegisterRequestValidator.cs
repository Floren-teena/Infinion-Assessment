using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Florentina_Infinion_Assessment.Application.DTOs;
using FluentValidation;

namespace Florentina_Infinion_Assessment.Application.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegisterRequestValidator()
        {
            // Strict validation for email only
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("Email is required")
                 .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage("Invalid email format");
        }
    }
}
