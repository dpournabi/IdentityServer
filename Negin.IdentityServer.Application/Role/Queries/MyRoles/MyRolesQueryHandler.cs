using IdentityServer.Application.Common.Interfaces;
using IdentityServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nsp.Common;

namespace IdentityServer.Application.Role.Queries.MyRoles;

public class MyRolesQueryHandler : IRequestHandler<MyRolesQuery, Result<IEnumerable<MyRoleResponse>>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public MyRolesQueryHandler(IApplicationDbContext dbContext, UserManager<ApplicationUser> signInManager)
    {
        _dbContext = dbContext;
        _userManager = signInManager;
    }
    public async Task<Result<IEnumerable<MyRoleResponse>>> Handle(MyRolesQuery request, CancellationToken cancellationToken)
    {
        ApplicationUser user = await _dbContext.ApplicationUsers.FirstAsync(x => x.UserName == request.UserName).ConfigureAwait(false);

        var roles = await (from ur in _dbContext.UserRoles
                           join r in _dbContext.ApplicationRoles
                                               .Include(x => x.Parent) on ur.RoleId equals r.Id
                           where ur.UserId == user.Id
                           select new MyRoleResponse
                           {
                               RoleName = r.Name,
                               ParentId = r.ParentId,
                               ParentRoleName = r.Parent != null ? r.Parent.Name : string.Empty
                           }).ToListAsync(cancellationToken);

        return Result<IEnumerable<MyRoleResponse>>.Success(roles);
    }
}
