using FluentValidation;
namespace IdentityServer.Application.User.Commands.UpdateUser;
public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator() 
    {
        RuleFor(x => x.UserName).NotEmpty().NotNull().WithMessage("نام کاربری الزامی می باشد");
        //RuleFor(x => x.CurrentPassword).NotEmpty().NotNull().WithMessage("کلمه عبور الزامی می باشد");
        RuleFor(x => x.NewPassword).NotEmpty().NotNull().WithMessage("تایید کلمه عبور الزامی می باشد");
        //RuleFor(x => x).Must(CompareCurrentPasswordAndNewPassword).WithMessage("کلمه عبور با تایید کلمه عبور یکسان نمی باشد");
    }
    //public bool CompareCurrentPasswordAndNewPassword(ChangePasswordCommand changePasswordCommand)
    //{
    //    return changePasswordCommand.CurrentPassword == changePasswordCommand.NewPassword;
    //}
}
