//using IdentityServer4.Models;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace IdentityServer.Domain.Entities;

//public class Client: IdentityServer4.Models.Client
//{
//    public int Id { get; set; }

//    public int? CompanyId { get; set; }
//    public Company? Company { get; set; }

//    [NotMapped]
//    public ICollection<string> AllowedCorsOrigins { get; set; }

//    //[NotMapped]
//    //public new ICollection<string> AllowedScopes { get; set; }

//    //[NotMapped]
//    //public new ICollection<Secret> ClientSecrets { get; set; }

//    //[NotMapped]
//    //public new ICollection<string> AllowedGrantTypes { get; set; }

//    //[NotMapped]
//    //public new ICollection<string> AllowedIdentityTokenSigningAlgorithms { get; set; }


//    //[NotMapped]
//    //public new ICollection<string> IdentityProviderRestrictions { get; set; }

//    //[NotMapped]
//    //public new ICollection<string> PostLogoutRedirectUris { get; set; }

//    //[NotMapped]
//    //public new ICollection<string> RedirectUris { get; set; }

//    //[NotMapped]
//    //public new IDictionary<string, string> Properties { get; set; }

//    //[NotMapped]
//    //public new ICollection<ClientClaim> Claims { get; set; }


//    private readonly List<BaseEvent> _domainEvents = new();

//    [NotMapped]
//    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

//    public void AddDomainEvent(BaseEvent domainEvent)
//    {
//        _domainEvents.Add(domainEvent);
//    }

//    public void RemoveDomainEvent(BaseEvent domainEvent)
//    {
//        _domainEvents.Remove(domainEvent);
//    }

//    public void ClearDomainEvents()
//    {
//        _domainEvents.Clear();
//    }
//}
