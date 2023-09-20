using NGOT.ApplicationCore.ValueObjects;
using NGOT.Common.Models;

namespace NGOT.ApplicationCore.Entities;

public class CarType : Entity<Guid>
{
    public string Name { get; set; } = null!;
    public Icon Icon { get; set; } = null!;

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}