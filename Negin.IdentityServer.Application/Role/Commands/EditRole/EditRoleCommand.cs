using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.Role.Commands.EditRole
{
    public class EditRoleCommand : IRequest<Result<bool>>
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
}
