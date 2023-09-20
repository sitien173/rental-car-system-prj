using NGOT.Common.Enums;
using NGOT.Common.Models;

namespace NGOT.ApplicationCore.Entities;

public class RentalContract : AuditableEntity<string>
{
    public string RentalRequestId { get; set; } = null!;
    public DateTime? CancellationDate { get; set; }
    public string? CancellationReason { get; set; }
    public DateTime? AccidentDate { get; set; }
    public string? AccidentDescription { get; set; }
    public DateTime? LateDate { get; set; }
    public string? LateReason { get; set; }
    public decimal Amount { get; set; }
    public decimal PrepaidAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public RentalContractStatusEnum Status { get; set; }

    // navigation properties
    public virtual RentalRequest RentalRequest { get; set; } = null!;
    public virtual List<Payment> Payments { get; set; } = new();
    public virtual List<DamageAssessment> DamageAssessments { get; set; } = new();
    public virtual List<VehicleHandover> VehicleHandovers { get; set; } = new();
}