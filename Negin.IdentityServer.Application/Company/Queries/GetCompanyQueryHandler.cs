using IdentityServer.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nsp.Common;

namespace IdentityServer.Application.Company.Queries;

public class GetCompanyQueryHandler : IRequestHandler<GetCompanyQuery, Result<IEnumerable<GetCompanyResponse>>>
{
    private readonly IApplicationDbContext _dbContext;
    public GetCompanyQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<IEnumerable<GetCompanyResponse>>> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
    {
        var companies = await _dbContext.Companies
                                        .Select(x => (GetCompanyResponse)x)
                                        .ToListAsync();
        return Result<IEnumerable<GetCompanyResponse>>.Success(companies);
    }
}
