using IdentityServer.Application.User.Commands.SignInUserByOtp;
using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.User.Commands.UserOtp
{
    public class VerifyUserOtpCommand : IRequest<Result<SigninByOtpResponse>>
    {
        public VerifyUserOtpCommand() { }
        public string UserName { get; set; }
        public string OtpCode { get; set; }
    }
}
