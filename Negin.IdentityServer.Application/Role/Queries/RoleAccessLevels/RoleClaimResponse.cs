namespace IdentityServer.Application.Role.Queries.RoleAccessLevels;
public class RoleClaimResponse
{
    public int Id { get; set; }
    public string? ClaimType { get; set; }
    public string? ClaimValue { get; set; }
}
