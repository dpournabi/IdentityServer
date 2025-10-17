using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.User.Queries.MyClaims
{
    public class GetMyClaimsQuery : IRequest<Result<IEnumerable<Domain.Entities.RoleClaim>>>
    {
        public string UserName { get; set; }
    }
}
