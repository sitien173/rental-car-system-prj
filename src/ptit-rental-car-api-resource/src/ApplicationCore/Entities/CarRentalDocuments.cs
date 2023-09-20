using NGOT.Common.Models;

namespace NGOT.ApplicationCore.Entities;

public class CarRentalDocuments : Entity
{
    public Guid CarId { get; set; }
    public Guid RentalDocumentId { get; set; }

    // navigation properties
    public virtual Car Car { get; set; } = null!;
    public virtual RentalDocuments RentalDocuments { get; set; } = null!;
}