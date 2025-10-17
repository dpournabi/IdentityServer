using System;

namespace IdentityServer.Domain.Entities;

public class Licence:BaseEntity<long>
{
    public  int PlanId { get; set; }
    public Plan plan { get; set; }

    public  int ApplicationId { get; set; }
    public Application Application { get; set; }

    public  int CompanyId { get; set; }
    public Company Company { get; set; }

    public int? DiscountTypeId { get; set; }
    public DiscountType? DiscountType { get; set; }

    public decimal? DiscountValue { get; set; }
    public  DateTime StartDate { get; set; }
    public  DateTime EndDate { get; set; }
    public  bool PaymentStatus { get; set; }
    public  bool IsActive { get; set; }
}
