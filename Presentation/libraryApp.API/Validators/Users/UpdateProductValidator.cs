
using FluentValidation;
using libraryApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.API.Validators.Users
{
    public class CreteUserValidator: AbstractValidator<CreateUserViewModel>
    {
        public CreteUserValidator()
        {
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

            RuleFor(p => p.Password)
            .NotEmpty().WithMessage("Lütfen şifre bilgisini boş geçmeyiniz.")
            .NotNull().WithMessage("Lütfen şifre bilgisini boş geçmeyiniz.");

            RuleFor(p => p.PasswordConfirm)
                .NotEmpty().WithMessage("Lütfen şifre onay bilgisini boş geçmeyiniz.")
                .NotNull().WithMessage("Lütfen şifre onay bilgisini boş geçmeyiniz.")
                .Equal(p => p.Password).WithMessage("Şifreler eşleşmiyor.");






        }
    }
}
