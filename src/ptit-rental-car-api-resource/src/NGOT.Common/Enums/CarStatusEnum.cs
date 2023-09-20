using System.ComponentModel;

namespace NGOT.Common.Enums;

public enum CarStatusEnum
{
    [Description("Đang sẵn có")] Available,
    [Description("Đã được thuê")] Rented,
    [Description("Đang bảo trì")] UnderMaintenance,
    [Description("Đã hỏng")] Damaged,
    [Description("Đã hủy")] Cancelled,
    [Description("Đã khóa")] Locked,
    [Description("Không sẵn có")] Unavailable
}