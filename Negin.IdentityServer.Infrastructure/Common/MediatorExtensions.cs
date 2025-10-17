using IdentityServer.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace MediatR;

public static class MediatorExtensions
{
    public static async Task DispatchDomainEvents(this IMediator mediator, DbContext context) 
    {
        var intEntities = context.ChangeTracker
            .Entries<BaseEntity<int>>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);

       var longEntities=context.ChangeTracker
            .Entries<BaseEntity<long>>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);

        var domainEvents = intEntities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        domainEvents.AddRange(longEntities
            .SelectMany(e => e.DomainEvents)
            .ToList());

        intEntities.ToList().ForEach(e => e.ClearDomainEvents());
        longEntities.ToList().ForEach(e => e.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}
