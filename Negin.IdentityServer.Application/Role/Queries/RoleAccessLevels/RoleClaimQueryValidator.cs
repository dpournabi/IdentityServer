using FluentValidation;

namespace IdentityServer.Application.Role.Queries.RoleAccessLevels;
public class RoleClaimQueryValidator : AbstractValidator<RoleClaimQuery>
{
    public RoleClaimQueryValidator()
    {
        RuleFor(x => x.RoleName).NotEmpty().NotNull().WithMessage("نام نقش الزامی می باشد");
    }
}
