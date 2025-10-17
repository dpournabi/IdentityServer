using MediatR;
using Nsp.Common;
namespace IdentityServer.Application.Role.Queries.MyRoles;

public class MyRolesQuery : IRequest<Result<IEnumerable<MyRoleResponse>>>
{
    public string UserName { get; set; }
}
