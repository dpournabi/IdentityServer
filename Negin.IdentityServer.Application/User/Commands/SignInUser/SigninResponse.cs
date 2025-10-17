namespace IdentityServer.Application.User.Commands.SignInUser;
public record SigninResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpireDate { get; set; }
}