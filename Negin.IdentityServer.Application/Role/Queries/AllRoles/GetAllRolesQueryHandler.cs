using IdentityServer.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nsp.Common;

namespace IdentityServer.Application.Role.Queries.AllRoles
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, Result<IEnumerable<GetAllRolesResponse>>>
    {
        private readonly IApplicationDbContext _dbContext;
        public GetAllRolesQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result<IEnumerable<GetAllRolesResponse>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            List<GetAllRolesResponse> allRoles = await _dbContext.ApplicationRoles
                                    .Where(x => (request.Phrase == null ||
                                                request.Phrase != null &&
                                                  (x.Name.Contains(request.Phrase) ||
                                                   x.LocalName.Contains(request.Phrase)
                                                  )
                                                ) &&
                                          x.Name != "Root")
                                    .Select(x => new GetAllRolesResponse
                                    {
                                        Id = x.Id,
                                        RoleName = x.Name,
                                        LocalizedName = x.LocalName,
                                        ParentId = x.ParentId
                                    })
                                    .ToListAsync()
                                    .ConfigureAwait(false);

            return Result<IEnumerable<GetAllRolesResponse>>.Success(allRoles);
        }
    }
}
