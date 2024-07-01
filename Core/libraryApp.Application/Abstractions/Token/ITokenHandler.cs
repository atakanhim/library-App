using libraryApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace libraryApp.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        Task<DTOs.Token> CreateAccessToken(int accessTokenLifeTimeSecond, AppUser appUser);
        string CreateRefreshToken();
    }
}
