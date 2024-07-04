using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libraryApp.Application.DTOs;

namespace libraryApp.Application.ResponseModels
{
    public class LoginResponse
    {
        public Token token { get; set; }
        public string userid { get; set; }
        public string username { get; set; }
    }
}
