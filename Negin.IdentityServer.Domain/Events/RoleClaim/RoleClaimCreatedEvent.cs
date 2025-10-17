namespace IdentityServer.Domain.Events.RoleClaim;

public class RoleClaimCreatedEvent : BaseEvent
{
    public RoleClaimCreatedEvent(Entities.RoleClaim item)
    {
        Item = item;
    }

    public Entities.RoleClaim Item { get; }
}
