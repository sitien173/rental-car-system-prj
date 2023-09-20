using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NGOT.Common.Enums;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.RentalContract;

public record RentalContractResponse
    : ISimpleMap<Entities.RentalContract, RentalContractResponse>
{
    public string Id { get; init; } = null!;
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
    public DateTime Created { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public RentalContractStatusEnum Status { get; set; }
}