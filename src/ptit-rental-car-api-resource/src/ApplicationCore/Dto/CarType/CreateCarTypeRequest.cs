using NGOT.ApplicationCore.ValueObjects;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.CarType;

public record CreateCarTypeRequest(string Name, Icon Icon) : ISimpleMap<CreateCarTypeRequest, Entities.CarType>;