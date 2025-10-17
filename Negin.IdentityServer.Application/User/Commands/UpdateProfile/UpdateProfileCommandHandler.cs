using IdentityServer.Application.Common.Interfaces;
using IdentityServer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nsp.Common;

namespace IdentityServer.Application.User.Commands.UpdateProfile;
public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateProfileCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
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

            if (!string.IsNullOrEmpty(request.FirstName))
            {
                applicationUser.FirstName = request.FirstName;
            }

            if (!string.IsNullOrEmpty(request.LastName))
            {
                applicationUser.LastName = request.LastName;
            }

            if (applicationUser.DateOfBirth != null)
            {
                applicationUser.DateOfBirth = request.DateOfBirth;
            }

            if (!string.IsNullOrEmpty(applicationUser.ImageUrl))
            {
                applicationUser.ImageUrl = request.ImageUrl;
            }

            if (!string.IsNullOrEmpty(applicationUser.PhoneNumber))
            {
                applicationUser.PhoneNumber = request.PhoneNumber;
            }

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
