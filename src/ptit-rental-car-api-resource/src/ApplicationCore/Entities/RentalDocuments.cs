using NGOT.Common.Models;

namespace NGOT.ApplicationCore.Entities;

public class RentalDocuments : Entity<Guid>
{
    public string Name { get; set; } = null!;

    // navigation properties
    public virtual List<CarRentalDocuments> CarRentalDocuments { get; set; } = new();
}