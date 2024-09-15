using Florentina_Infinion_Assessment.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florentina_Infinion_Assessment.Application.Helpers.Interfaces
{
    public interface IEmailService
    {
        Task RegistrationEmailAsync(EmailDto emailDto);
    }
}
