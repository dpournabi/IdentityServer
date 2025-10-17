using System.Security.Claims;

namespace IdentityServer.Application.User.Commands.SignInUserByOtp
{
    public record SigninByOtpResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpireDate { get; set; }
        public string TokenType { get; set; }
        public bool ConfirmByAdmin { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
    }
}
