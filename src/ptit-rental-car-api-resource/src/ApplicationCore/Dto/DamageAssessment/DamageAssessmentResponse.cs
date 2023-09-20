using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.DamageAssessment;

public record DamageAssessmentResponse(Guid Id,
    DateTime AssessmentDate,
    string DamageDescription,
    decimal RepairCost,
    decimal TotalCost,
    string RentalContractId
) : ISimpleMap<Entities.DamageAssessment, DamageAssessmentResponse>;