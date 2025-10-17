namespace IdentityServer.Domain.Entities;

public class IndustryCompany:BaseEntity<int>
{
    public  int IndustryId { get; set; }
    public  int CompanyId { get; set; }

    public Industry Industry { get; set; }
    public Company Company { get; set; }
}
