using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.Groups.Commands
{
    public class AddRoleGroupsCommand : IRequest<Result<bool>>
    {
        public Guid RoleId { get; set; }
        public int[] Groups { get; set; }
    }
}
