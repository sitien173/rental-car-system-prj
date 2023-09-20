using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NGOT.ApplicationCore.Dto.CheckList;
using NGOT.Common.Enums;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.VehicleHandover;

public record VehicleHandoverResponse(Guid Id,
        DateTime HandoverDate,
        string HandoverBy,
        [property: JsonConverter(typeof(StringEnumConverter))]
        HandoverTypeEnum HandoverType,
        string RentalContractId,
        IEnumerable<CheckListItemResponse> CheckListItems
    )
    : ISimpleMap<Entities.VehicleHandover, VehicleHandoverResponse>;