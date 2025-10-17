using IdentityServer.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nsp.Common;

namespace IdentityServer.Application.User.Queries.UserRoles
{
    public class UserRoleQueryHandler : IRequestHandler<UserRoleQuery, Result<List<string>>>
    {
        private readonly IApplicationDbContext _dbContext;

        public UserRoleQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<List<string>>> Handle(UserRoleQuery request, CancellationToken cancellationToken)
        {
            List<string> roles = await (from u in _dbContext.ApplicationUsers
                                        join ur in _dbContext.UserRoles on u.Id equals ur.UserId
                                        join r in _dbContext.ApplicationRoles on ur.RoleId equals r.Id
                                        where u.UserName == request.UserName
                                        select r.Name).ToListAsync();

            return Result<List<string>>.Success(roles);
        }
    }
}
