using IdentityServer.Application.Common.Interfaces;
using IdentityServer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nsp.Common;

namespace IdentityServer.Application.User.Commands.UpdateProfile;
public class UnlockUserCommandHandler : IRequestHandler<UnlockUserCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;

    public UnlockUserCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Handle(UnlockUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            ApplicationUser? applicationUser = await _dbContext.ApplicationUsers
                                               .FirstOrDefaultAsync(x => x.UserName == request.UserName && x.IsActive)
                                               .ConfigureAwait(false);
            if (applicationUser is null)
            {
                return Result.Failure(new List<string> { "نام کاربری اشتباه می باشد" });
            }

            applicationUser.LockoutEnd = null;
            applicationUser.AccessFailedCount = 0;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            return Result.Failure(new List<string> { message });
        }
    }
}
