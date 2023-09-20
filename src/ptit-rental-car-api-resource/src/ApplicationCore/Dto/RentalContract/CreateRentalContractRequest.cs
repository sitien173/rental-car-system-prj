using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.RentalContract;

public record CreateRentalContractRequest
    : ISimpleMap<CreateRentalContractRequest, Entities.RentalContract>
{
    public string RentalRequestId { get; set; }
    public decimal Amount { get; set; }
    public decimal PrepaidAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}