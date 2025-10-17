using IdentityServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Nsp.Common;

namespace IdentityServer.Application.User.Commands.SignOutUser;
public class SignOutCommandHandler : IRequestHandler<SignOutCommand, Result>
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public SignOutCommandHandler(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<Result> Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        await _signInManager.SignOutAsync();
        return await Task.FromResult(Result.Success());
    }
}