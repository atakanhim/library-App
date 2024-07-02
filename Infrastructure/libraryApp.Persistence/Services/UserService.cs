using Azure;
using libraryApp.Application.Abstractions.Services;
using libraryApp.Application.DTOs.User;
using libraryApp.Application.Exceptions;
using libraryApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;



namespace libraryApp.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        readonly UserManager<libraryApp.Domain.Entities.Identity.AppUser> _userManager;
        readonly RoleManager<libraryApp.Domain.Entities.Identity.AppRole> _roleManager;
        public UserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, ILogger<UserService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public int TotalUsersCount => throw new NotImplementedException();

        public Task AssignRoleToUserAsnyc(string userId, string[] roles)
        {
            throw new NotImplementedException();
        }
        public async Task UpdateAsync(UpdateUserDTO model)
        { 
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            var usr = await _userManager.FindByIdAsync(model.Id);
            if (usr != null)
            {
                // burda istiyorumki modelden gelen veriler boş degilse degiştirsin
                if (!string.IsNullOrEmpty(model.Email))          
                    usr.Email = model.Email;
                if (!string.IsNullOrEmpty(model.FullName))
                    usr.FullName = model.FullName;
                if (!string.IsNullOrEmpty(model.Username))
                    usr.UserName = model.Username;

                var result = await _userManager.UpdateAsync(usr);
                if (result.Succeeded)
                    _logger.LogInformation("User Update işlemi gerçekleştirildi Update edilen user ID:" + usr.Id);


                else
                {
                    var err = "";
                    // Update failed, handle errors
                    foreach (var error in result.Errors)
                    {
                        err += ' ' + error.Description;
                        // Log or display error.Description
                    }
                    throw new (err);
                }
            }
            else
            {
                throw new ("Gecersiz ID Degeri");
            }


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
                        if (!await _roleManager.RoleExistsAsync("User"))
                        {
                            await _roleManager.CreateAsync(new AppRole()
                            {
                                Id = Guid.NewGuid().ToString(), // Generate a new GUID for the Id
                                Name = "User",
                                ConcurrencyStamp = Guid.NewGuid().ToString(), // Generate a new GUID for the ConcurrencyStamp
                                NormalizedName = "USER"
                            });

                        }
                        var addToRoleResult = await _userManager.AddToRoleAsync(user, "User");
  
                    }
                    else
                    {
                        response.Message += " Ama  Kullanıcı bulunamadı User Rolu eklenemedi.";
                    }


                   _logger.LogInformation(response.Message);
                 }

                else
                {
                    foreach (var error in result.Errors)
                            response.Message += $"{error.Code} - {error.Description}\n";
                    throw new Exception(response.Message);
                }
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
