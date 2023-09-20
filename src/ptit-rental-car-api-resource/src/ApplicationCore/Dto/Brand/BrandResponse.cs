using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.Brand;

public class BrandResponse : ISimpleMap<Entities.Brand, BrandResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}