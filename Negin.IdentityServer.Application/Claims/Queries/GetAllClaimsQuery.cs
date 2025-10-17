using IdentityServer.Domain.Entities;
using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.Claims.Queries;

public class GetAllClaimsQuery : IRequest<Result<IEnumerable<ClientClaims>>>
{
}
