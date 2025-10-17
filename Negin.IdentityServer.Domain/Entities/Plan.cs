namespace IdentityServer.Domain.Entities;

public class Plan:BaseEntity<int>
{
    public  int PlanTypeId { get; set; }
    public  string Title { get; set; }
    public  decimal Amount { get; set; }
    public  int ResourceSize { get; set; }
    public  int DurationMonth { get; set; }

    public PlanType PlanType { get; set; }
}
