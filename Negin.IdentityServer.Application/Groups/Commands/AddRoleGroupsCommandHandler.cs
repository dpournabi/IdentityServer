using IdentityServer.Application.Common.Interfaces;
using IdentityServer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nsp.Common;

namespace IdentityServer.Application.Groups.Commands
{
    public class AddRoleGroupsCommandHandler : IRequestHandler<AddRoleGroupsCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _dbContext;
        public AddRoleGroupsCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<bool>> Handle(AddRoleGroupsCommand request, CancellationToken cancellationToken)
        {
            List<RoleGroup> existing = await this._dbContext.RoleGroups.Where(g => g.ApplicationRoleId == request.RoleId).ToListAsync();

            this._dbContext.RoleGroups.RemoveRange(existing);
            await this._dbContext.SaveChangesAsync(cancellationToken);

            List<RoleGroup> list = new();
            foreach (int item in request.Groups)
            {
                list.Add(new RoleGroup() { ApplicationRoleId = request.RoleId, GroupId = item });
            }

            this._dbContext.RoleGroups.AddRange(list);
            await this._dbContext.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true, "اطلاعات با موفقیت ثبت شد.");
        }
    }
}