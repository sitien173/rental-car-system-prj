using NGOT.Common.Interfaces;

namespace NGOT.Common.Models;

public class AuditableEntity<TKey> : Entity<TKey>, IAuditable
{
    public DateTime Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}