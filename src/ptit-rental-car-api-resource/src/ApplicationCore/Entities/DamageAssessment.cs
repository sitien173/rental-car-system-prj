using NGOT.Common.Models;

namespace NGOT.ApplicationCore.Entities;

public class DamageAssessment : Entity<Guid>
{
    public DateTime AssessmentDate { get; set; }
    public string DamageDescription { get; set; } = null!;
    public decimal RepairCost { get; set; }
    public decimal TotalCost { get; set; }
    public string RentalContractId { get; set; } = null!;
    public RentalContract RentalContract { get; set; } = null!;
}