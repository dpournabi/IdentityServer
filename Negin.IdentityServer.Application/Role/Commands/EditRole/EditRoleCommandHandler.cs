using IdentityServer.Application.Common.Interfaces;
using MediatR;
using Nsp.Common;
using System.Data;

namespace IdentityServer.Application.Role.Commands.EditRole
{
    public class EditRoleCommandHandler : IRequestHandler<EditRoleCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _dbContext;
        public EditRoleCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<bool>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.ApplicationRole? existing = this._dbContext.ApplicationRoles.Where(r => r.Id == request.Id).FirstOrDefault();
            if (existing is null)
            {
                return Result<bool>.Failure(new string[] { "Not Found!" }, false);
            }

            existing.LocalName = request.Name;
            existing.Name = request.Name;
            existing.NormalizedName = request.Name.ToUpper();

            this._dbContext.ApplicationRoles.Update(existing);
            await this._dbContext.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true, "نقش با موفقیت ذخیره شد");
        }
    }
}
