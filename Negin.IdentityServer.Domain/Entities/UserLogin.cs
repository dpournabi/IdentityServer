using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Domain.Entities;

public class UserLogin : IdentityUserLogin<Guid>
{
    public DateTime LoginDate => DateTime.UtcNow;
}
