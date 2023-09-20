using NGOT.ApplicationCore.ValueObjects;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.CarType;

public record UpdateCarTypeRequest(string? Name, Icon? Icon)
    : ISimpleMap<UpdateCarTypeRequest, Entities.CarType>;