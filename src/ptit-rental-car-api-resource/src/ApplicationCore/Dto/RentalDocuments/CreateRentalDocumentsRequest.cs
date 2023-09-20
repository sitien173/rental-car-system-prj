using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.RentalDocuments;

public record CreateRentalDocumentsRequest(string Name)
    : ISimpleMap<CreateRentalDocumentsRequest, Entities.RentalDocuments>;