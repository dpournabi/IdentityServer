using IdentityServer.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nsp.Common;

namespace IdentityServer.Application.Company.Commands.RemoveCompeny
{
    public class DeleteCompanyCammandHandler : IRequestHandler<DeleteCompanyCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteCompanyCammandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result<bool>> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Domain.Entities.Company? existing = await _dbContext.Companies.Where(r => r.Id == request.Id).FirstOrDefaultAsync();
                if (existing is null)
                {
                    return Result<bool>.Failure(new string[] { "Not Found!" }, false);

                }

                _dbContext.Companies.Remove(existing);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return Result<bool>.Success(true, "شرکت با موفقیت حذف شد");
            }

            catch (Exception)
            {

                return Result<bool>.Success(false, "این شرکت را نمیتوان حذف کرد");
            }
        }
    }
}
