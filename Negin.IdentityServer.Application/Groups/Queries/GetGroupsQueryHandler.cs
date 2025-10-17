using IdentityServer.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nsp.Common;

namespace IdentityServer.Application.Groups.Queries
{
    public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, Result<IEnumerable<GetRoleGroupResponse>>>
    {
        private readonly IApplicationDbContext _dbContext;
        public GetGroupsQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<GetRoleGroupResponse>>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.Group> groups = await this._dbContext.Groups.ToListAsync();
            List<Domain.Entities.RoleGroup> roleGroups = await this._dbContext.RoleGroups.Where(g => g.ApplicationRoleId == request.RoleId).ToListAsync();

            List<GetRoleGroupResponse> result = new();
            foreach (Domain.Entities.Group? item in groups)
            {
                bool exists = roleGroups.Exists(g => g.GroupId == item.Id);

                result.Add(new GetRoleGroupResponse()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Checked = exists
                });
            }

            return Result<IEnumerable<GetRoleGroupResponse>>.Success(result);
        }
    }
}