namespace IdentityServer.Domain.Entities;

public class DiscountType:BaseEntity<int>
{
    public  string TitleEn { get; set; }
    public  string Title { get; set; }
}
