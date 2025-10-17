using IdentityServer.Application.Common.Interfaces;
using IdentityServer.Domain.Entities;
using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.Role.Commands.AddRole
{
    public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _dbContext;
        public AddRoleCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<bool>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _dbContext.ApplicationRoles.Add(new ApplicationRole()
                {
                    LocalName = request.Name,
                    Name = request.Name,
                    NormalizedName = request.Name.ToUpper(),
                    ParentId = request.ParentId
                });
                await this._dbContext.SaveChangesAsync(cancellationToken);

                return Result<bool>.Success(true, "نقش با موفقیت ثبت شد");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(new string[] { ex.GetBaseException().Message }, false);
            }
        }
    }
}
