using libraryApp.Application.ResponseModels;


namespace libraryApp.Application.Abstractions.Services.Authentications
{
    public interface IInternalAuthentication
    {
        Task<LoginResponse> LoginAsync(string usernameOrEmail, string password);
        Task<DTOs.Token> RefreshTokenLoginAsync(string refreshToken);
    }
}
