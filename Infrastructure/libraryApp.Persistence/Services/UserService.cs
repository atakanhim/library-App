using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using libraryApp.Application.Abstractions.Services;
using libraryApp.Application.DTOs.User;
using libraryApp.Domain.Entities.Identity;



namespace libraryApp.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<libraryApp.Domain.Entities.Identity.AppUser> _userManager;
        readonly RoleManager<libraryApp.Domain.Entities.Identity.AppRole> _roleManager;
        public UserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public int TotalUsersCount => throw new NotImplementedException();

        public Task AssignRoleToUserAsnyc(string userId, string[] roles)
        {
            throw new NotImplementedException();
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUserDTO model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName,
                RefreshToken = "",// create aşamasında bos olamaz hatası alıyoruz 
                RefreshTokenEndDate = DateTime.UtcNow,
            }, model.Password);
            
            CreateUserResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
            {
                response.Message = "Kullanıcı başarıyla oluşturulmuştur.";

                // Kullanıcı oluşturulduğunda bir role atamak isterseniz:
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user != null)
                {
                    // Örneğin "User" rolüne atama yapabiliriz

                    var addToRoleResult = await _userManager.AddToRoleAsync(user, "User");

                    if (addToRoleResult.Succeeded)
                    {
                        response.Message += " Kullanıcıya rol ataması yapıldı.";
                    }
                    else
                    {
                        response.Message += " Ancak rol ataması başarısız oldu.";
                        // Hata mesajlarını da işleyebilirsiniz
                        //response.Errors = addToRoleResult.Errors.Select(e => e.Description).ToList();
                    }
                }
                else
                {
                    response.Message += " Kullanıcı bulunamadı.";
                }
            }
               
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";

            return response;
        }

        public async Task<List<ListUserDTO>> GetAllUsersAsync(int page, int size)
        {
            try
            {
                // Kullanıcıların sayfalı olarak alınması
                var users = await _userManager.Users
                                             .Skip((page - 1) * size)
                                             .Take(size)
                                             .ToListAsync();


                var userList = users.Select(user => new ListUserDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                }).ToList();

                return userList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string[]> GetRolesToUserAsync(string userIdOrName)
        {
            throw new NotImplementedException();
        }

       
        public async Task<ListUserDTO> GetUser(string userName)
        {
            try
            {
                var user = await _userManager.Users
                             .Where(x => x.UserName == userName)
                             .FirstOrDefaultAsync();

                if (user == null)
                    throw new ("Kullanıcı bulunamadı");


               // ICollection<EmployeDTOIncludeAll> dtoemploye = _mapper.Map<ICollection<Employe>, ICollection<EmployeDTOIncludeAll>>(user.Employees);

                return new()
                {
                    Id = user.Id,
                    UserName = user.UserName!,
                    Email = user.Email!,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }     
        public async Task<AppUser> GetUserWithId(string id)
        {
            try
            {
                var user = await _userManager.Users
                             .Where(x => x.Id == id)
                             .FirstOrDefaultAsync();

                if (user == null)
                    throw new ("Kullanıcı bulunamadı");
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

 
        public async Task<bool> IsUserExists(string usurId)
        {
            var model = await  _userManager.FindByIdAsync(usurId);
            return model != null;
        }

        public Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateRefreshTokenAsync(string? refreshToken, AppUser user, DateTime? accessTokenDate, int addOnAccessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = (DateTime)(accessTokenDate?.AddSeconds(addOnAccessTokenDate));
                await _userManager.UpdateAsync(user);
            }
            else
                throw new ("kullanici bulunamadi");
        }

    }
}
