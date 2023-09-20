using NGOT.ApplicationCore.Entities;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.AdditionalFee;

public record AdditionalFeesResponse(Guid Id, string Name, string? Description, decimal Price, string? Unit)
    : ISimpleMap<AdditionalFees, AdditionalFeesResponse>;