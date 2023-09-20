using NGOT.Common.Enums;
using NGOT.Common.Models;

namespace NGOT.ApplicationCore.Entities;

public class CheckListItem : Entity<Guid>
{
    public string Name { get; set; } = null!;
    public CheckListItemStatusEnum Status { get; set; }
    public string? Comment { get; set; }
    public Guid VehicleHandoverId { get; set; }
    public VehicleHandover VehicleHandover { get; set; } = null!;
}