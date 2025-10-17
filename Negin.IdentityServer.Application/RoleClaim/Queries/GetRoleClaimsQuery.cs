using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.RoleClaim.Queries
{
    public class GetRoleClaimsQuery : IRequest<Result<IEnumerable<GetRoleClaimsResponse>>>
    {
        public Guid RoleId { get; set; }
    }
}
