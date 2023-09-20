using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.RentalDocuments;

public record UpdateRentalDocumentsRequest(string Name)
    : ISimpleMap<UpdateRentalDocumentsRequest, Entities.RentalDocuments>;