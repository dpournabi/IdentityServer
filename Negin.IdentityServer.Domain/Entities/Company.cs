namespace IdentityServer.Domain.Entities;

public class Company : BaseEntity<int>
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string RegisterNo { get; set; }
    public string Name { get; set; }
    public string? TellPhone { get; set; }
    public string? EconomicCode { get; set; }
    public string? NationalCode { get; set; }
    public string? CEOCell { get; set; }
    public string? CEOFirstName { get; set; }
    public string? CEOLastName { get; set; }
    public DateTime? CEOBirthday { get; set; }
    public string? Address { get; set; }

    public Company? Parent { get; set; }
}
