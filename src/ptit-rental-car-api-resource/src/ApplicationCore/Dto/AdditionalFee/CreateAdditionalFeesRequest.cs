using NGOT.ApplicationCore.Entities;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.AdditionalFee;

public record CreateAdditionalFeesRequest(string Name, string? Description, decimal Price, string? Unit)
    : ISimpleMap<CreateAdditionalFeesRequest, AdditionalFees>;