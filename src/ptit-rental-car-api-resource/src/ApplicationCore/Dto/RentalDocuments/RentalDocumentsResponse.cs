using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Dto.RentalDocuments;

public record RentalDocumentsResponse(Guid Id, string Name)
    : ISimpleMap<Entities.RentalDocuments, RentalDocumentsResponse>;