using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.DamageAssessment;

public record CreateDamageAssessmentRequest(
    DateTime AssessmentDate,
    string DamageDescription,
    decimal RepairCost,
    decimal TotalCost,
    string RentalContractId
) : ISimpleMap<CreateDamageAssessmentRequest, Entities.DamageAssessment>;