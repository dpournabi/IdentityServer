using IdentityServer.Application.Common.Interfaces;
using IdentityServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Nsp.Common;

namespace IdentityServer.Application.User.Commands.VerifyClient
{
    public class VerifyClientCommand : IRequest<Result>
    {
        public Guid UserId { get; set; }
        public bool? Verify { get; set; }
    }

    public class VerifyClientCommandHandler : IRequestHandler<VerifyClientCommand, Result>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public VerifyClientCommandHandler(IApplicationDbContext dbContext, SignInManager<ApplicationUser> signInManager)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
        }

        public async Task<Result> Handle(VerifyClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.UserId == Guid.Empty)
                    return Result.Failure(new string[] { });

                ApplicationUser user = await _signInManager.UserManager.FindByIdAsync(request.UserId.ToString()).ConfigureAwait(false);
                if (user == null)
                {
                    return Result.Failure(new string[] { "نام کاربری اشتباه می باشد" });
                }

                //Confirm user by admin
                //user.ConfirmByAdmin = true;
                user.IsActive = request.Verify.HasValue && request.Verify.Value;
                await _signInManager.UserManager.UpdateAsync(user);

                return Result.Success("تایید کاربر با موفقیت انجام گردید");
            }
            catch (Exception ex)
            {
                return Result.Failure(new string[] { ex.GetBaseException().Message });
            }
        }
    }
}
