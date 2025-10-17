using System;

namespace IdentityServer.Domain.Entities;

public class Actor:BaseEntity<long>
{
    public  Guid RoleId { get; set; }
    public  Guid UserId { get; set; }
    public DateTime? ExpireDate { get; set; }

    public ApplicationRole ApplicationRole { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
}
