using NGOT.Common.Models;

namespace NGOT.ApplicationCore.Entities;

public class Brand : Entity<Guid>
{
    public string Name { get; set; } = null!;

    // navigation properties
    public virtual List<Car> Cars { get; set; } = new();
}