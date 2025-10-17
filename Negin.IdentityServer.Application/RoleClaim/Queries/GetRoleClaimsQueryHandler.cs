using IdentityServer.Application.Common.Interfaces;
using IdentityServer8.EntityFramework.Entities;
using IdentityServer8.EntityFramework.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nsp.Common;

namespace IdentityServer.Application.RoleClaim.Queries
{
    public class GetRoleClaimsQueryHandler : IRequestHandler<GetRoleClaimsQuery, Result<IEnumerable<GetRoleClaimsResponse>>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IConfigurationDbContext _configurationDbContext;
        public GetRoleClaimsQueryHandler(IApplicationDbContext dbContext,
            IConfigurationDbContext configurationDbContext)
        {
            _dbContext = dbContext;
            _configurationDbContext = configurationDbContext;
        }

        public async Task<Result<IEnumerable<GetRoleClaimsResponse>>> Handle(GetRoleClaimsQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.RoleClaim> currentClaims = await this._dbContext.RoleClaims.Where(rc => rc.RoleId == request.RoleId).ToListAsync();
            List<ClientClaim> result = await this._configurationDbContext.Clients.SelectMany(x => x.Claims).ToListAsync();
            List<GetRoleClaimsResponse> response = new();

            foreach (ClientClaim? item in result)
            {
                response.Add(new GetRoleClaimsResponse()
                {
                    RoleId = request.RoleId,
                    ClaimType = item.Type,
                    ClaimValue = item.Value,
                    Checked = currentClaims.Any(c => c.ClaimType == item.Type && c.ClaimValue == item.Value)
                });
            }

            return Result<IEnumerable<GetRoleClaimsResponse>>.Success(response);
        }
    }
}
