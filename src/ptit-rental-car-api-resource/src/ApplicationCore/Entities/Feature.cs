using NGOT.ApplicationCore.ValueObjects;
using NGOT.Common.Models;

namespace NGOT.ApplicationCore.Entities;

public class Feature : Entity<Guid>
{
    public string Name { get; set; } = null!;
    public Icon Icon { get; set; } = null!;

    // navigation properties
    public virtual List<CarFeature> CarFeatures { get; set; } = new();
}