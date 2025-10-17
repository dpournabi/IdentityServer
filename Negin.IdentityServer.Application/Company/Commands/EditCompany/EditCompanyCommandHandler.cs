using IdentityServer.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nsp.Common;

namespace IdentityServer.Application.Company.Commands.EditCompany
{
    public class EditCompanyCommandHandler : IRequestHandler<EditCompanyCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _dbContext;
        public EditCompanyCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<bool>> Handle(EditCompanyCommand request, CancellationToken cancellationToken)
        {
            var find = await _dbContext.Companies.Where(t => t.Id == request.Id).FirstOrDefaultAsync();
            if (find != null)
            {

                find.Name = request.Name;
                find.NationalCode = request.NationalCode;
                find.Address = request.Address;
                find.CEOBirthday = request.CEOBirthday;
                find.Address = request.Address;
                find.CEOCell = request.CeoCell;
                find.CEOFirstName = request.CeoFirstName;
                find.CEOLastName = request.CeoLastName;
                find.RegisterNo = request.RegisterNo;
                find.TellPhone = request.TellPhone;
                find.EconomicCode = request.EconomicCode;
                _dbContext.Companies.Update(find);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return Result<bool>.Success(true, "شرکت با موفقیت ویرایش شد");

            }
            else
            {
                return Result<bool>.Success(false, "شرکت یافت نشد");

            }
        }
    }
}
