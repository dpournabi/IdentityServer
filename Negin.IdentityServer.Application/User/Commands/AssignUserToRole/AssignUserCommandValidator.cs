using FluentValidation;

namespace IdentityServer.Application.User.Commands.AssignUserToRole;
public class AssignUserCommandValidator : AbstractValidator<AssignUserCommand>
{
    public AssignUserCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().NotNull().WithMessage("نام کاربری الزامی می باشد");
        RuleFor(x => x.RoleName).NotEmpty().NotNull().WithMessage("نام نقش الزامی می باشد");
    }
}
