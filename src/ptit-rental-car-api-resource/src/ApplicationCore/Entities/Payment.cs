using NGOT.Common.Enums;
using NGOT.Common.Models;

namespace NGOT.ApplicationCore.Entities;

public class Payment : Entity<Guid>
{
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethodEnum PaymentMethod { get; set; }
    public PaymentStatusEnum Status { get; set; }
    public PaymentTypeEnum PaymentType { get; set; }

    public string RentalContractId { get; set; } = null!;

    // navigation properties
    public virtual RentalContract RentalContract { get; set; } = null!;
}