using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NGOT.ApplicationCore.Dto.AdditionalFee;
using NGOT.ApplicationCore.Dto.Brand;
using NGOT.ApplicationCore.Dto.CarType;
using NGOT.ApplicationCore.Dto.Feature;
using NGOT.ApplicationCore.Dto.RentalDocuments;
using NGOT.ApplicationCore.ValueObjects;
using NGOT.Common.Enums;

namespace NGOT.ApplicationCore.Dto.Car;

public record CarResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public CarSpecificity Specificity { get; init; }
    public string Description { get; init; }
    public BrandResponse Brand { get; init; }
    public CarTypeResponse CarType { get; init; }
    public decimal Price { get; init; }
    public string Rule { get; init; }

    [JsonConverter(typeof(StringEnumConverter))]
    public CarStatusEnum Status { get; init; }

    public IEnumerable<FeatureResponse> Features { get; init; } = Enumerable.Empty<FeatureResponse>();
    public IEnumerable<Image> Images { get; init; } = Enumerable.Empty<Image>();
    public IEnumerable<AdditionalFeeResponse> AdditionalFees { get; init; } = Enumerable.Empty<AdditionalFeeResponse>();

    public IEnumerable<RentalDocumentsResponse> RentalDocuments { get; init; } =
        Enumerable.Empty<RentalDocumentsResponse>();
}