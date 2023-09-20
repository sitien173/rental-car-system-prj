using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NGOT.Common.Enums;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.RentalRequest;

public record RentalRequestResponse(string Id, Guid UserId, Guid CarId, DateTime StartDate, DateTime EndDate,
        [property: JsonConverter(typeof(StringEnumConverter))]
        RentalRequestEnum Status)
    : ISimpleMap<Entities.RentalRequest, RentalRequestResponse>;