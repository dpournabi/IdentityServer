using FluentValidation;

namespace IdentityServer.Application.User.Queries.UserRoles;
public class UserRoleQueryValidator : AbstractValidator<UserRoleQuery>
{
    public UserRoleQueryValidator()
    {
        RuleFor(x => x).NotEmpty().NotNull().WithMessage("مدل ارسالی صحیح نمی باشد");
        RuleFor(x => x.UserName).NotEmpty().NotNull().WithMessage("نام کاربری الزامی می باشد");
    }
}
