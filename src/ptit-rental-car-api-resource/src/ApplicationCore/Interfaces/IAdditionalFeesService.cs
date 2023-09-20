using NGOT.ApplicationCore.Dto.AdditionalFee;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Interfaces;

public interface IAdditionalFeesService : IScoped
{
    Task<Guid> CreateAsync(CreateAdditionalFeesRequest request, CancellationToken ct = default);
    Task<AdditionalFeesResponse?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<AdditionalFeesResponse>> GetAllAsync(CancellationToken ct = default);
    Task<AdditionalFeesResponse> UpdateAsync(Guid id, UpdateAdditionalFeesRequest update, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
    Task DeleteAsync(Guid[] ids, CancellationToken ct);
}