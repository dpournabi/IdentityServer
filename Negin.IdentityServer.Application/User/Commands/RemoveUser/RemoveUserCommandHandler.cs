using IdentityServer.Application.Common.Interfaces;
using IdentityServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nsp.Common;

namespace IdentityServer.Application.User.Commands.RemoveUser;
public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly SignInManager<ApplicationUser> _signInManager;
    public RemoveUserCommandHandler(SignInManager<ApplicationUser> signInManager,
        IApplicationDbContext dbContext)
    {
        _signInManager = signInManager;
        _dbContext = dbContext;
    }
    public async Task<Result> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            ApplicationUser? applicationUser = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.UserName == request.UserName &&
                                                                                                x.IsActive).ConfigureAwait(false);
            if (applicationUser == null)
            {
                return Result.Failure(new List<string> { "نام کاربری اشتباه می باشد" });
            }

            applicationUser.IsActive = false;

            await _signInManager.UserManager.UpdateAsync(applicationUser);
            return Result.Success();
        }
        catch (Exception ex)
        {
            string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            return Result.Failure(new List<string> { message });
        }
    }
}
