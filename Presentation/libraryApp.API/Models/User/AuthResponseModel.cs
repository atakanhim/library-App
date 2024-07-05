using libraryApp.Application.DTOs;

namespace libraryApp.API.Models.User
{
    public class AuthResponseModel
    {
        public Token Token { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool Success { get; set; }
    }
}
