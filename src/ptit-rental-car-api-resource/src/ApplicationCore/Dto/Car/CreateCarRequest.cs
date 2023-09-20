using NGOT.ApplicationCore.ValueObjects;

namespace NGOT.ApplicationCore.Dto.Car;

public record CreateCarRequest(
    string Name,
    CarSpecificity Specificity,
    string Description,
    Guid BrandId,
    Guid CarTypeId,
    decimal Price,
    string Rule
)
{
    public IEnumerable<Image> Images { get; init; } = Enumerable.Empty<Image>();
    public IEnumerable<Guid> FeatureIds { get; init; } = Enumerable.Empty<Guid>();
    public IEnumerable<Guid> AdditionalFeeIds { get; init; } = Enumerable.Empty<Guid>();
    public IEnumerable<Guid> RentalDocumentIds { get; init; } = Enumerable.Empty<Guid>();
}