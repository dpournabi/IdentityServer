using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.User.Queries.UserRoles;
public class UserRoleQuery : IRequest<Result<List<string>>>
{
    public string UserName { get; set; }
}
