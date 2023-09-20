using NGOT.ApplicationCore.Dto.CheckList;
using NGOT.Common.Enums;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.VehicleHandover;

public record CreateVehicleHandoverRequest(
        HandoverTypeEnum HandoverType,
        string RentalContractId,
        IEnumerable<CreateCheckListItemRequest> CheckListItems
    )
    : ISimpleMap<CreateVehicleHandoverRequest, Entities.VehicleHandover>;