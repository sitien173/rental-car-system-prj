using NGOT.Common.Models;

namespace NGOT.ApplicationCore.Entities;

public class AdditionalFees : Entity<Guid>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? Unit { get; set; }

    // navigation properties
    public virtual List<CarAdditionalFees> CarAdditionalFees { get; set; } = new();
}