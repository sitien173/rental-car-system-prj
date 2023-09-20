using NGOT.Common.Enums;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.RentalRequest;

public record UpdateRentalRequest(RentalRequestEnum Status) : ISimpleMap<UpdateRentalRequest, Entities.RentalRequest>;