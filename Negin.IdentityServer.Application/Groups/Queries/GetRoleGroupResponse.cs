namespace IdentityServer.Application.Groups.Queries
{
    public record GetRoleGroupResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
    }
}
