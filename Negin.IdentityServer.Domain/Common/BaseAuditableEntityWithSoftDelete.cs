namespace IdentityServer.Domain.Common;

public abstract class BaseAuditableEntityWithSoftDelete<T> : BaseEntity<T>
{
    public DateTime Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    public bool IsDelete { get; set; }
}
