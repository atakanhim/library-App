
using FluentValidation;
using libraryApp.API.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.API.Validators.Users
{
    public class UpdateProductValidator: AbstractValidator<UpdateUserViewModel>
    {
        public UpdateProductValidator()
        {
            RuleFor(p => p.Id)
               .NotEmpty()
               .NotNull()
               .WithMessage("Kullanıcı ID'si boş geçilemez")
               .Must(BeAValidGuid)
                    .WithMessage("Kullanıcı ID'si geçerli bir GUID olmalıdır");

            RuleFor(p => p.Username)
                .NotEmpty()
                .NotNull()
                    .WithMessage("lutfen  kullanıcı adını  bos gecmeyiniz")
                .MaximumLength(100)
                .MinimumLength(3)
                    .WithMessage("lutfen  kullanıcı adını 5 ile 130 arasında girin");
            RuleFor(p => p.FullName)
             .NotEmpty()
             .NotNull()
                 .WithMessage("lutfen  adınızı  bos gecmeyiniz")
             .MaximumLength(100)
             .MinimumLength(3)
                 .WithMessage("lutfen  adınızı 5 ile 130 arasında girin");
            RuleFor(p => p.Email)
                .EmailAddress()
                .NotNull()
                .NotEmpty()
                    .WithMessage("lütfen email adresini dogru ve eksiksiz girin");

           

        }
        private bool BeAValidGuid(string id)
        {
            return Guid.TryParse(id, out _);
        }
    }
}
