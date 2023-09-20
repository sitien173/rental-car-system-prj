using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NGOT.ApplicationCore.Entities;
using NGOT.Common.Enums;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.CheckList;

public record CreateCheckListItemRequest(
    string Name,
    [property: JsonConverter(typeof(StringEnumConverter))]
    CheckListItemStatusEnum Status,
    string? Comment) :
    ISimpleMap<CreateCheckListItemRequest, CheckListItem>;