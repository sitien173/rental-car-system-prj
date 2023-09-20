using NGOT.Common.Models;

namespace NGOT.ApplicationCore.Entities;

public class CarAdditionalFees : Entity
{
    public Guid CarId { get; set; }
    public Guid AdditionalFeesId { get; set; }

    // navigation properties
    public virtual Car Car { get; set; } = null!;
    public virtual AdditionalFees AdditionalFees { get; set; } = null!;
}