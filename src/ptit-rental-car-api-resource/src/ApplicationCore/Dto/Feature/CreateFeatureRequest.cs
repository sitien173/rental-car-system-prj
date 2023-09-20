using NGOT.ApplicationCore.ValueObjects;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.Feature;

public record CreateFeatureRequest(string Name, Icon Icon) : ISimpleMap<CreateFeatureRequest, Entities.Feature>;