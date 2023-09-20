using NGOT.ApplicationCore.Entities;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.AdditionalFee;

public record UpdateAdditionalFeesRequest(string? Name, string? Description, decimal? Price, string? Unit)
    : ISimpleMap<UpdateAdditionalFeesRequest, AdditionalFees>;