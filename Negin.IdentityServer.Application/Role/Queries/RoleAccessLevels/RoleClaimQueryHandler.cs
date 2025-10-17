using IdentityServer.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nsp.Common;

namespace IdentityServer.Application.Role.Queries.RoleAccessLevels;

public class RoleClaimQueryHandler : IRequestHandler<RoleClaimQuery, Result<List<RoleClaimResponse>>>
{
    private readonly IApplicationDbContext _dbContext;

    public RoleClaimQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<RoleClaimResponse>>> Handle(RoleClaimQuery request, CancellationToken cancellationToken)
    {
        List<RoleClaimResponse> roleClaims = await (from rc in _dbContext.RoleClaims
                                              join r in _dbContext.ApplicationRoles on rc.RoleId equals r.Id
                                              where r.Name == request.RoleName &&
                                                    rc.ClaimType == "Permission"
                                              select new RoleClaimResponse
                                              {
                                                  Id = rc.Id,
                                                  ClaimType = rc.ClaimType,
                                                  ClaimValue = rc.ClaimValue
                                              }).ToListAsync();

        return Result<List<RoleClaimResponse>>.Success(roleClaims);
    }
}
