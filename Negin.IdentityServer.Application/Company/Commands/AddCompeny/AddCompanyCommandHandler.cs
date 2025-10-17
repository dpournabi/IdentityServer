using IdentityServer.Application.Common.Interfaces;
using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.Company.Commands.AddCompeny
{
    public class AddCompanyCommandHandler : IRequestHandler<AddCompanyCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _dbContext;
        public AddCompanyCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result<bool>> Handle(AddCompanyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var add = await _dbContext.Companies.AddAsync(new Domain.Entities.Company()
                {
                    Address = request.Address,
                    CEOBirthday = request.CEOBirthday,
                    CEOCell = request.CeoCell,
                    Name = request.Name,
                    CEOFirstName = request.CeoFirstName,
                    CEOLastName = request.CeoLastName,
                    EconomicCode = request.EconomicCode,
                    TellPhone = request.TellPhone,
                    RegisterNo = request.RegisterNo
                });
                await _dbContext.SaveChangesAsync(cancellationToken);
                return Result<bool>.Success(true, "شرکت با موفقیت ثبت شد");
            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
