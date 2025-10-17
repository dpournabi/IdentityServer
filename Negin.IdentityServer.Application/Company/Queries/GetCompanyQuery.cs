using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.Company.Queries;

public record GetCompanyQuery : IRequest<Result<IEnumerable<GetCompanyResponse>>>
{
}
