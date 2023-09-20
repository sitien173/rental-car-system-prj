using NGOT.Common.Models;

namespace NGOT.ApplicationCore.Entities;

public class CarFeature : Entity
{
    public Guid CarId { get; set; }
    public Guid FeatureId { get; set; }

    public virtual Car Car { get; set; } = null!;
    public virtual Feature Feature { get; set; } = null!;
}