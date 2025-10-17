using IdentityServer.Application.Common.Interfaces;
using IdentityServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nsp.Common;
using System.Text;

namespace IdentityServer.Application.User.Commands.UpdateUser;
public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IApplicationDbContext _dbContext;

    public ChangePasswordCommandHandler(SignInManager<ApplicationUser> signInManager,
                                        IApplicationDbContext dbContext)
    {
        _signInManager = signInManager;
        _dbContext = dbContext;
    }

    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        ApplicationUser? applicationUser = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.UserName == request.UserName &&
                                                                                                x.IsActive).ConfigureAwait(false);
        if (applicationUser == null)
        {
            return Result.Failure(new List<string> { "نام کاربری اشتباه می باشد" });
        }

        if (request.NewPassword != request.ConfirmationPassword)
        {
            return Result.Failure(new List<string> { "رمز عبور و تأیید رمز عبور باید یکسان باشند!" });
        }

        applicationUser.PasswordHash = _signInManager.UserManager.PasswordHasher.HashPassword(applicationUser, request.NewPassword);
        await this._dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
