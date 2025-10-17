using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.User.Commands.RemoveUser;
public class RemoveUserCommand : IRequest<Result>
{
    public string UserName { get; set; }
}
