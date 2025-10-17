using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.Role.Commands.AddRole
{
    public record AddRoleCommand(string Name, Guid? ParentId) : IRequest<Result<bool>>
    {
    }
}
