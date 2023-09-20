using NGOT.ApplicationCore.ValueObjects;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.Feature;

public record UpdateFeatureRequest(string? Name, Icon? Icon)
    : ISimpleMap<UpdateFeatureRequest, Entities.Feature>;