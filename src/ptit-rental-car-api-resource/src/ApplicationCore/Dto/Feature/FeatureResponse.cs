using NGOT.ApplicationCore.ValueObjects;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.Feature;

public record FeatureResponse(Guid Id, string Name, Icon Icon)
    : ISimpleMap<Entities.Feature, FeatureResponse>;