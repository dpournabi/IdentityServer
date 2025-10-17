namespace IdentityServer.Application.User.Queries.MyProfile;

public record UserProfileResponse
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ImageUrl { get; set; }
    public long? BranchId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string[] UserRoles { get; set; }
}
