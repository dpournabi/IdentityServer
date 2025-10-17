using Microsoft.AspNetCore.Identity; using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServer.Domain.Entities;  public class ApplicationUser : IdentityUser<Guid> {
    public int? CompanyId { get; set; }     public int? BranchId { get; set; }     public Branch Branch { get; set; }
    public Company Company { get; set; }      public string? FirstName { get; set; }     public string? LastName { get; set; }     public DateTime? DateOfBirth { get; set; }     public string? ImageUrl { get; set; }      public bool IsActive { get; set; }

    [NotMapped]     public IdentityUserRole<Guid> ApplicationUserRole { get; set; }      private readonly List<BaseEvent> _domainEvents = new();

    [NotMapped]
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
    } } 