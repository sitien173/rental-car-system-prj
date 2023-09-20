using NGOT.ApplicationCore.Dto.RentalDocuments;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Interfaces;

public interface IRentalDocumentsService : IScoped
{
    Task<Guid> CreateAsync(CreateRentalDocumentsRequest request, CancellationToken ct = default);
    Task<RentalDocumentsResponse?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<RentalDocumentsResponse>> GetAllAsync(CancellationToken ct = default);
    Task<RentalDocumentsResponse> UpdateAsync(Guid id, UpdateRentalDocumentsRequest update, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
    Task DeleteAsync(Guid[] ids, CancellationToken ct);
}