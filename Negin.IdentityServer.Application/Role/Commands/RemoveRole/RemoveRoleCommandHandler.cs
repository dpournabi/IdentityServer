using IdentityServer.Application.Common.Interfaces;
using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.Role.Commands.RemoveRole
{
    public class RemoveRoleCommandHandler : IRequestHandler<RemoveRoleCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _dbContext;

        public RemoveRoleCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result<bool>> Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Domain.Entities.ApplicationRole? existing = _dbContext.ApplicationRoles.Where(r => r.Id == request.Id).FirstOrDefault();
                if (existing is null)
                {
                    return Result<bool>.Failure(new string[] { "Not Found!" }, false);
                }

                _dbContext.ApplicationRoles.Remove(existing);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return Result<bool>.Success(true, "نقش با موفقیت حذف شد");
            }


            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
