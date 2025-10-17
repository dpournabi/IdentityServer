using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.Role.Commands.RemoveRole
{
    public class RemoveRoleCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
    }
}
