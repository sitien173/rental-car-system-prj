using NGOT.Common.Enums;
using NGOT.Common.Models;

namespace NGOT.ApplicationCore.Entities;

public class VehicleHandover : Entity<Guid>
{
    public DateTime HandoverDate { get; set; }
    public string HandoverBy { get; set; } = null!;
    public HandoverTypeEnum HandoverType { get; set; }
    public string RentalContractId { get; set; } = null!;
    public RentalContract RentalContract { get; set; } = null!;
    public virtual List<CheckListItem> CheckListItems { get; set; } = new();
}