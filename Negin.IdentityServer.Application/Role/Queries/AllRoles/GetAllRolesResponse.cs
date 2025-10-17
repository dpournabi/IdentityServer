namespace IdentityServer.Application.Role.Queries.AllRoles;
public record GetAllRolesResponse
{
    public Guid Id { get; set; }
    public string RoleName { get; set; }
    public string LocalizedName { get; set; }
    public Guid? ParentId { get; set; }
}
