using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Application.DTOs.User
{
    public class UserDTO : IdentityUser<string>
    {
        public string? FullName { get; set; }
        
    }
}
