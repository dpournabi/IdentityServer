using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.Role.Queries.AllRoles
{
    public class GetAllRolesQuery : IRequest<Result<IEnumerable<GetAllRolesResponse>>>
    {
        public string? Phrase { get; set; }
    }
}
