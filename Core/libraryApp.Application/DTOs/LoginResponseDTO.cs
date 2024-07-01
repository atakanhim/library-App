using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Application.DTOs
{
    public class LoginResponseDTO
    {
        public Token token { get; set; }
        public string userid { get; set; }
        public string username { get; set; }
    }
}
