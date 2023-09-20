using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.Brand;

public record UpdateBrandRequest(string Name) : ISimpleMap<UpdateBrandRequest, Entities.Brand>;