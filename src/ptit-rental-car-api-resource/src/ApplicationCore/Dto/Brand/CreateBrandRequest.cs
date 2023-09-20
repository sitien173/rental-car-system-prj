using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.Brand;

public record CreateBrandRequest(string Name) : ISimpleMap<CreateBrandRequest, Entities.Brand>;