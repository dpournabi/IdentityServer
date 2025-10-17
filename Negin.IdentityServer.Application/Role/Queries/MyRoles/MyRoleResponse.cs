namespace IdentityServer.Application.Role.Queries.MyRoles;

public record MyRoleResponse
{
    public string RoleName { get; init; }
    public Guid? ParentId { get; init; }
    public string? ParentRoleName { get; init; }
}
