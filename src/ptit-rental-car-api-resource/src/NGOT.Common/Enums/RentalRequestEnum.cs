using System.ComponentModel;

namespace NGOT.Common.Enums;

public enum RentalRequestEnum
{
    [Description("Đang chờ duyệt")] Pending,
    [Description("Đã được duyệt")] Approved,
    [Description("Đang thuê")] Active,
    [Description("Đã bị từ chối")] Rejected,
    [Description("Đã hủy")] Canceled,
    [Description("Đã hoàn thành")] Completed
}