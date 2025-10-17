using FluentValidation;

namespace IdentityServer.Application.User.Commands.UserOtp;

public class VerifyUserOtpCommandValidator : AbstractValidator<VerifyUserOtpCommand>
{
    public VerifyUserOtpCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().NotNull().WithMessage("نام کاربری الزامی می باشد");
    }
}
