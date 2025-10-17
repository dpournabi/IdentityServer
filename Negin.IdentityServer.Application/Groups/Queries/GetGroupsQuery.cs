using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.Groups.Queries
{
    public class GetGroupsQuery : IRequest<Result<IEnumerable<GetRoleGroupResponse>>>
    {
        public Guid RoleId { get; set; }
    }
}
