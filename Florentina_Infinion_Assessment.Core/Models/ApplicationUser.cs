using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florentina_Infinion_Assessment.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        //public string Email { get; set; }
        //public string PasswordHash { get; set; }
        //public bool EmailConfirmed { get; set; }
    }
}
