using NGOT.ApplicationCore.ValueObjects;
using NGOT.Common.Enums;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.Car;

public record UpdateCarRequest(
    string? Name,
    CarSpecificity? Specificity,
    string? Description,
    Guid? BrandId,
    Guid? CarTypeId,
    decimal? Price,
    string? Rule,
    CarStatusEnum Status
) : ISimpleMap<UpdateCarRequest, Entities.Car>
{
    public IEnumerable<Image> Images { get; init; } = Enumerable.Empty<Image>();
    public IEnumerable<Guid> FeatureIds { get; init; } = Enumerable.Empty<Guid>();
    public IEnumerable<Guid> AdditionalFeeIds { get; init; } = Enumerable.Empty<Guid>();
    public IEnumerable<Guid> RentalDocumentIds { get; init; } = Enumerable.Empty<Guid>();
}