using NGOT.Common.Enums;
using NGOT.Common.Models;

namespace NGOT.ApplicationCore.Entities;

public class RentalRequest : Entity<string>
{
    public Guid UserId { get; set; }
    public Guid CarId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public RentalRequestEnum Status { get; set; }

    // navigation properties
    public virtual Car Car { get; set; } = null!;
    public virtual RentalContract? RentalContract { get; set; }
    public string? RentalContractId { get; set; }
}