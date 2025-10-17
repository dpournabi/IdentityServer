using IdentityServer8.Models;
using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.RoleClaim.Commands
{
    public class AddRoleClaimCommand : IRequest<Result<bool>>
    {
        public Guid RoleId { get; set; }
        public IEnumerable<ClientClaim> Claims { get; set; }
    }
}
