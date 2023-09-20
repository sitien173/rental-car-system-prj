using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NGOT.ApplicationCore.Entities;
using NGOT.Common.Enums;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.CheckList;

public record CheckListItemResponse(Guid Id,
    string Name,
    [property: JsonConverter(typeof(StringEnumConverter))]
    CheckListItemStatusEnum Status,
    string? Comment,
    Guid VehicleHandoverId) :
    ISimpleMap<CheckListItem, CheckListItemResponse>;