using libraryApp.Application.DTOs;


namespace libraryApp.Application.Abstractions.Services.Authentications
{
    public interface IInternalAuthentication
    {
        Task<LoginResponseDTO> LoginAsync(string usernameOrEmail, string password);
        Task<DTOs.Token> RefreshTokenLoginAsync(string refreshToken,int refreshTokenLifeTimeSecond);
    }
}
