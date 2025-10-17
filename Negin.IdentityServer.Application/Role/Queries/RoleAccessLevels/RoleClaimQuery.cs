using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.Role.Queries.RoleAccessLevels;
public class RoleClaimQuery : IRequest<Result<List<RoleClaimResponse>>>
{
    public string RoleName { get; set; }
}
