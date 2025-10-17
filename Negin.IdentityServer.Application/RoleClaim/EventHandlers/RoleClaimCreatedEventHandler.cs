using IdentityServer.Domain.Events.RoleClaim;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IdentityServer.Application.RoleClaim.EventHandlers
{
    public class RoleClaimCreatedEventHandler : INotificationHandler<RoleClaimCreatedEvent>
    {
        private readonly ILogger<RoleClaimCreatedEventHandler> _logger;
        public RoleClaimCreatedEventHandler(ILogger<RoleClaimCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(RoleClaimCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Negin system identity server --> Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
