using NGOT.ApplicationCore.ValueObjects;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.CarType;

public record CarTypeResponse(Guid Id, string Name, Icon Icon)
    : ISimpleMap<Entities.CarType, CarTypeResponse>;