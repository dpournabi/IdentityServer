using FluentValidation;
using IdentityServer.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Application.User.Commands.SignInUserByOtp
{
    public class SigninByOtpCommandValidator : AbstractValidator<SigninByOtpCommand>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private ApplicationUser _user;
        public SigninByOtpCommandValidator(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;

            RuleFor(x => x.UserName).NotEmpty().NotNull().WithMessage("نام کاربری الزامی می باشد");
            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("کلمه عبور الزامی می باشد");
            RuleFor(x => x.ClientId).NotEmpty().NotNull().WithMessage("ClientId الزامی می باشد");
            RuleFor(x => x.UserName).Must(CheckUserIsUnLock).WithMessage("نام کاربری شما بدلیل ورود پسوردهای اشتباه قفل شده است");
            RuleFor(x => x.UserName).Must(CheckUserIsActive).WithMessage("نام کاربری شما فعال نمی باشد");
            RuleFor(x => x.UserName).Must(CheckUserIsNotNull).WithMessage("نام کاربری یا رمز عبور شما صحیح نمی باشد");
        }

        public ApplicationUser SetCurrentUser(string userName)
        {
            if (_user is not null && _user.UserName == userName)
            {
                return _user;
            }

            _user = _signInManager.UserManager.FindByNameAsync(userName).Result;
            return _user;
        }
        private bool CheckUserIsNotNull(string userName)
        {
            SetCurrentUser(userName);
            return _user is not null;
        }

        private bool CheckUserIsActive(string userName)
        {
            SetCurrentUser(userName);
            return _user is { IsActive: true };
        }

        private bool CheckUserIsUnLock(string userName)
        {
            SetCurrentUser(userName);
            return _user is { LockoutEnd: null };
        }
    }
}
