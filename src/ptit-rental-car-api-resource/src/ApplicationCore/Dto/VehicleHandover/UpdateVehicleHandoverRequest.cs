using NGOT.ApplicationCore.Dto.CheckList;
using NGOT.Common.Enums;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.VehicleHandover;

public record UpdateVehicleHandoverRequest(
    DateTime? HandoverDate,
    string? HandoverBy,
    HandoverTypeEnum? HandoverType,
    string? RentalContractId,
    IEnumerable<CreateCheckListItemRequest> CheckListItems
) : ISimpleMap<UpdateVehicleHandoverRequest, Entities.VehicleHandover>;