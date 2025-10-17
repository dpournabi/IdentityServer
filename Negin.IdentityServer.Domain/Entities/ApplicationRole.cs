using Microsoft.AspNetCore.Identity;  namespace IdentityServer.Domain.Entities;  public class ApplicationRole : IdentityRole<Guid> {     public string LocalName { get; set; }     public Guid? ParentId { get; set; }      public virtual ApplicationRole? Parent { get; set; }     public virtual ICollection<ApplicationRole> ChildrenRoles { get; set; }     private readonly List<BaseEvent> _domainEvents = new();

    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    } } 