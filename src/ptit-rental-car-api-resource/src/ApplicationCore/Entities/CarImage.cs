using NGOT.ApplicationCore.ValueObjects;
using NGOT.Common.Models;

namespace NGOT.ApplicationCore.Entities;

public class CarImage : Entity<Guid>
{
    public Guid CarId { get; set; }
    public Image Image { get; set; } = null!;
    public bool IsMain { get; set; }

    // navigation properties
    public virtual Car Car { get; set; } = null!;
}