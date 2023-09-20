using NGOT.ApplicationCore.Dto.RentalRequest;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Interfaces;

public interface IRentalRequestService : IScoped
{
    Task<IEnumerable<RentalRequestResponse>> GetAllAsync(CancellationToken ct);
    Task<RentalRequestResponse> UpdateAsync(string id, UpdateRentalRequest request, CancellationToken ct);
    Task DeleteAsync(string id, CancellationToken ct);
    Task DeleteAsync(string[] ids, CancellationToken ct);
}