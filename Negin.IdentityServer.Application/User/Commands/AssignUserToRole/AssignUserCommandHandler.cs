using IdentityServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Nsp.Common;

namespace IdentityServer.Application.User.Commands.AssignUserToRole;
public class AssignUserCommandHandler : IRequestHandler<AssignUserCommand, Result>
{

    private readonly SignInManager<ApplicationUser> _signInManager;

    public AssignUserCommandHandler(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }
    public async Task<Result> Handle(AssignUserCommand request, CancellationToken cancellationToken)
    {

        ApplicationUser? user = await _signInManager.UserManager.FindByNameAsync(request.UserName).ConfigureAwait(false);
        if (user is null)
        {
            return Result.Failure(new List<string> { "User not found!" });
        }

        IdentityResult result = await _signInManager.UserManager.AddToRoleAsync(user, request.RoleName);
        if (!result.Succeeded)
        {
            return Result.Failure(new List<string> { $"Assign user {request.UserName} to role {request.RoleName} was unsuccessfully!" });
        }

        return Result.Success();
    }
}
