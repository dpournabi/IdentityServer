using IdentityServer.Application.Common.Interfaces;
using IdentityServer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nsp.Common;

namespace IdentityServer.Application.User.Queries.MyProfile
{
    public class MyProfileQueryHandler : IRequestHandler<MyProfileQuery, Result<UserProfileResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public MyProfileQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<UserProfileResponse>> Handle(MyProfileQuery request, CancellationToken cancellationToken)
        {
            ApplicationUser user = await _dbContext.ApplicationUsers.FirstAsync(x => x.UserName == request.UserName).ConfigureAwait(false);
            var roles = (from ur in _dbContext.UserRoles
                            join r in _dbContext.ApplicationRoles on ur.RoleId equals r.Id
                         where ur.UserId == user.Id
                         select r.LocalName).ToArray();

            if (user != null)
            {
                UserProfileResponse profile = new()
                {
                    UserName = user.UserName,
                    BranchId = user.BranchId,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    Id = user.Id,
                    LastName = user.LastName,
                    ImageUrl = user.ImageUrl,
                    PhoneNumber = user.PhoneNumber,
                    UserRoles = roles
                };

                return Result<UserProfileResponse>.Success(profile);
            }

            return Result<UserProfileResponse>.Failure(Array.Empty<string>());
        }
    }
}
