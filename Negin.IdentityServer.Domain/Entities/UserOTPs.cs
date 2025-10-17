namespace IdentityServer.Domain.Entities;

public class UserOTPs : BaseEntity<long>
{
    public Guid UserId { get; set; }
    public string OtpCode { get; set; }
    public DateTime ExpireDate { get; set; }
    public string MessageBody { get; set; }
    public bool IsRecieved { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime TokenExpireDate { get; set; }
    public string TokenType { get; set; }
    public bool OtpCodeVerified { get; set; }
    public ApplicationUser User { get; set; }
}
