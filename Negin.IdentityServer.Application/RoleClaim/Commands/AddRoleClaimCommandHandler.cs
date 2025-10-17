using IdentityServer.Application.Common.Interfaces;
using IdentityServer.Domain.Events.RoleClaim;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nsp.Common;

namespace IdentityServer.Application.RoleClaim.Commands
{
    public class AddRoleClaimCommandHandler : IRequestHandler<AddRoleClaimCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _dbContext;
        public AddRoleClaimCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<bool>> Handle(AddRoleClaimCommand request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.RoleClaim> currentClaims = await _dbContext.RoleClaims.Where(rc => rc.RoleId == request.RoleId).ToListAsync();
            List<Domain.Entities.RoleClaim> newClaims = request.Claims.Select(x => new Domain.Entities.RoleClaim
            {
                RoleId = request.RoleId,
                ClaimType = x.Type,
                ClaimValue = x.Value
            }).ToList();

            _dbContext.RoleClaims.RemoveRange(currentClaims);
            await _dbContext.SaveChangesAsync(cancellationToken);

            foreach (Domain.Entities.RoleClaim currentClaim in newClaims)
            {
                currentClaim.AddDomainEvent(new RoleClaimCreatedEvent(currentClaim));
            }
            _dbContext.RoleClaims.AddRange(newClaims);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true, "دسترسی ها با موفقیت ذخیره شدند.");
        }
    }
}
