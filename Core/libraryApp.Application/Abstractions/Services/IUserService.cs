using libraryApp.Application.DTOs.User;
using libraryApp.Domain.Entities.Identity;



namespace libraryApp.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<bool> IsUserExists(string usurId);
        Task<CreateUserResponse> CreateAsync(CreateUserDTO model);
        Task<ListUserDTO> GetUser(string userName);
        Task<AppUser> GetUserWithId(string id);
        Task UpdateRefreshTokenAsync(string? refreshToken, AppUser user, DateTime? accessTokenDate, int addOnAccessTokenDate);
        Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
        Task AssignRoleToUserAsnyc(string userId, string[] roles);
        Task<string[]> GetRolesToUserAsync(string userIdOrName);
    }
}
