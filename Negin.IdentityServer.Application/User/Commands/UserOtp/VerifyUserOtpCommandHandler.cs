using IdentityServer.Application.Common.Interfaces;
using IdentityServer.Application.User.Commands.SignInUserByOtp;
using IdentityServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nsp.Common;

namespace IdentityServer.Application.User.Commands.UserOtp
{
    public class VerifyUserOtpCommandHandler : IRequestHandler<VerifyUserOtpCommand, Result<SigninByOtpResponse>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public VerifyUserOtpCommandHandler(IApplicationDbContext dbContext,
            SignInManager<ApplicationUser> signInManager)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
        }
        public async Task<Result<SigninByOtpResponse>> Handle(VerifyUserOtpCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.ApplicationUser? user = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.UserName == request.UserName);
            if (user is null)
            {
                return Result<SigninByOtpResponse>.Failure(new List<string> { "Invalid username!" });
            }

            UserOTPs? userOtp = await _dbContext.UserOTPs
                                          .FirstOrDefaultAsync(x => x.UserId == user.Id &&
                                          x.ExpireDate > DateTime.Now);

            if (string.Compare(userOtp.MessageBody.Trim(), request.OtpCode.Trim(), true) != 0)
            {
                return Result<SigninByOtpResponse>.Failure(new List<string> { "Invalid two step verification code!" }, null);
            }

            userOtp.OtpCodeVerified = true;
            await _dbContext.SaveChangesAsync(cancellationToken);

            SigninByOtpResponse signinResponse = new()
            {
                AccessToken = userOtp.AccessToken,
                TokenType = userOtp.TokenType,
                ExpireDate = userOtp.TokenExpireDate,
                RefreshToken = userOtp.RefreshToken,
                Claims = await _signInManager.UserManager.GetClaimsAsync(user).ConfigureAwait(false)
            };
            return await Task.FromResult(Result<SigninByOtpResponse>.Success(signinResponse));
        }
    }
}
