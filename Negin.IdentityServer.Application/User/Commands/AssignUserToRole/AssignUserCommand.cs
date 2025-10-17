using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.User.Commands.AssignUserToRole;
public class AssignUserCommand : IRequest<Result>
{
    public string UserName { get; set; }
    public string RoleName { get; set; }
}
