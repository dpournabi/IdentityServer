using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.User.Commands.SignInUserByOtp
{
    public record SigninByOtpCommand : IRequest<Result<SigninByOtpResponse>>
    {
        public string UserName { get; init; }
        public string Password { get; init; }
        public string OtpCode { get; set; }
        public string ClientId { get; init; }
        public string ClientSecret { get; init; }
        public string Scope { get; init; }
        public bool RememberLogin { get; init; }
    }
}
