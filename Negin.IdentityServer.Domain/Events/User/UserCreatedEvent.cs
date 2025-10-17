namespace IdentityServer.Domain.Events.User
{
    public class UserCreatedEvent : BaseEvent
    {
        public UserCreatedEvent(ApplicationUser item)
        {
            Item = item;
        }

        public ApplicationUser Item { get; }
    }
}
