using NGOT.Common.Enums;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.RentalContract;

public record UpdateRentalContractRequest
    : ISimpleMap<UpdateRentalContractRequest, Entities.RentalContract>
{
    public DateTime? CancellationDate { get; init; }
    public string? CancellationReason { get; init; }
    public DateTime? AccidentDate { get; init; }
    public string? AccidentDescription { get; init; }
    public DateTime? LateDate { get; init; }
    public string? LateReason { get; init; }
    public RentalContractStatusEnum? Status { get; init; }
}