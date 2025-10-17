using IdentityServer.Application.Common.Interfaces;
using IdentityServer.Domain.Entities;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nsp.Common;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Application.User.Commands.SaveUserRoles
{
    public class SaveUserRolesCommand: IRequest<Result<bool>>
    {
        public string Username { get; set; }
        public IEnumerable<CheckedRoles> Roles { get; set; }
    }

    public class SaveUserRolesCommandHandler : IRequestHandler<SaveUserRolesCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public SaveUserRolesCommandHandler(IApplicationDbContext dbContext, SignInManager<ApplicationUser> signInManager)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
        }

        public async Task<Result<bool>> Handle(SaveUserRolesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!request.Roles.Any())
                    return Result<bool>.Failure(new string[] { });

                var user = await _signInManager.UserManager.FindByNameAsync(request.Username).ConfigureAwait(false);

                if (user != null)
                {
                    Guid[] assigningRoleIds = request.Roles.Where(r => r.Checked).Select(r => r.Id).ToArray();

                    var removingRoles = await this._dbContext.UserRoles.Where(r => r.UserId == user.Id).ToListAsync();

                    var assigningRoles = assigningRoleIds.Select(r => new UserRole()
                    {
                        UserId = user.Id,
                        RoleId = r
                    });

                    this._dbContext.UserRoles.RemoveRange(removingRoles);
                    await this._dbContext.SaveChangesAsync(cancellationToken);

                    this._dbContext.UserRoles.AddRange(assigningRoles);
                    await this._dbContext.SaveChangesAsync(cancellationToken);

                    return Result<bool>.Success(true, "تعیین نقش با موفقیت انجام شد.");
                }
                else
                {
                    return Result<bool>.Failure(new string[] { "نام کاربری اشتباه می باشد" });
                }
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(new string[] { ex.GetBaseException().Message });
            }
        }
    }
}
