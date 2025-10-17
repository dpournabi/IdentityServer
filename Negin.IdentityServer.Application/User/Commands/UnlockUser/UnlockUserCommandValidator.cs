using FluentValidation;

namespace IdentityServer.Application.User.Commands.UpdateProfile;
public class UnlockUserCommandValidator : AbstractValidator<UpdateProfileCommand>
{
    public UnlockUserCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().NotNull().WithMessage("نام کاربری الزامی می باشد");
    }
}
