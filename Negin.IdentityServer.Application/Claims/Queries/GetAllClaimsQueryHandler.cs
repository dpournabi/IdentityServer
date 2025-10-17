using IdentityServer.Application.Common.Interfaces;
using IdentityServer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nsp.Common;

namespace IdentityServer.Application.Claims.Queries
{
    public class GetAllClaimsQueryHandler : IRequestHandler<GetAllClaimsQuery, Result<IEnumerable<ClientClaims>>>
    {
        private readonly IApplicationDbContext _dbContext;
        public GetAllClaimsQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<ClientClaims>>> Handle(GetAllClaimsQuery request, CancellationToken cancellationToken)
        {
            List<ClientClaims> result = await this._dbContext.ClientClaims.ToListAsync();

            return Result<IEnumerable<ClientClaims>>.Success(result);
        }
    }
}
