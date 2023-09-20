using NGOT.ApplicationCore.Entities;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.AdditionalFee;

public record AdditionalFeeResponse(Guid Id, string Name, string? Description, decimal Price, string? Unit)
    : ISimpleMap<AdditionalFees, AdditionalFeeResponse>;