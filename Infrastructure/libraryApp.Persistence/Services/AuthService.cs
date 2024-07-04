using libraryApp.Application.Abstractions.Services;
using libraryApp.Application.Abstractions.Token;
using libraryApp.Application.DTOs;
using libraryApp.Application.Exceptions;
using libraryApp.Application.ResponseModels;
using libraryApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace libraryApp.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly IConfiguration _configuration;
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
        readonly IUserService _userService;
        public AuthService(
            IConfiguration configuration,
            UserManager<Domain.Entities.Identity.AppUser> userManager,
            ITokenHandler tokenHandler,
            SignInManager<AppUser> signInManager,
            IUserService userService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
            _userService = userService;
        }
        
        public async Task<LoginResponse> LoginAsync(string usernameOrEmail, string password)
        {
            var accessTokenLifeTimeSecond = 15000;
            var refreshTokenLifeTimeSecond = 15000;
            Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(usernameOrEmail);

            if (user == null)
                throw new NotFoundUserException();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded) //Authentication başarılı!
            {
                Token tokenn = await _tokenHandler.CreateAccessToken(accessTokenLifeTimeSecond, user);
                await _userService.UpdateRefreshTokenAsync(tokenn.RefreshToken, user, tokenn.Expiration, refreshTokenLifeTimeSecond);
                return new (){ 
                    token = tokenn,
                    userid = user.Id,  
                    username=user.UserName
                };
            }
            throw new AuthenticationErrorException();
        }

  
        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)// var olan refreh token kullnarak yeni bir token olusturur ve onunda üzerine koyarak yeni bir resfresh token olusturur.
        {
            int second = 15000;
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = await _tokenHandler.CreateAccessToken(second, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, second);
                return token;
            }
            else
                throw new NotFoundUserException();
        }

    }
}
