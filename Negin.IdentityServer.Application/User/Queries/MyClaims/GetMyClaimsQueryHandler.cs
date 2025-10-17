using IdentityServer.Application.Common.Interfaces;
using IdentityServer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nsp.Common;

namespace IdentityServer.Application.User.Queries.MyClaims
{
    public class GetMyClaimsQueryHandler : IRequestHandler<GetMyClaimsQuery, Result<IEnumerable<Domain.Entities.RoleClaim>>>
    {
        private readonly IApplicationDbContext _context;
        public GetMyClaimsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<Domain.Entities.RoleClaim>>> Handle(GetMyClaimsQuery request, CancellationToken cancellationToken)
        {
            ApplicationUser user = await _context.ApplicationUsers.FirstAsync(x => x.UserName == request.UserName);

            List<Guid> roleIds = await _context.UserRoles
                                                 .Where(ur => ur.UserId == user.Id)
                                                 .Select(x => x.RoleId)
                                                 .ToListAsync();

            List<Domain.Entities.RoleClaim> roleClaims = await _context.RoleClaims
                                       .Where(rc => roleIds.Contains(rc.RoleId))
                                       .ToListAsync();

            return Result<IEnumerable<Domain.Entities.RoleClaim>>.Success(roleClaims);
        }
    }
}