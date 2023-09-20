using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.DamageAssessment;

public record UpdateDamageAssessmentRequest(DateTime? AssessmentDate,
    string? DamageDescription,
    decimal? RepairCost,
    decimal? TotalCost,
    string? RentalContractId
) : ISimpleMap<UpdateDamageAssessmentRequest, Entities.DamageAssessment>;