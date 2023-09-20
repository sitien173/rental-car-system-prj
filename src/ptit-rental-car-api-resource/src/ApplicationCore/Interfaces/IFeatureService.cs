using NGOT.ApplicationCore.Dto.Feature;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Interfaces;

public interface IFeatureService : IScoped
{
    Task<Guid> CreateAsync(CreateFeatureRequest request, CancellationToken ct = default);
    Task<FeatureResponse?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<FeatureResponse>> GetAllAsync(CancellationToken ct = default);
    Task<FeatureResponse> UpdateAsync(Guid id, UpdateFeatureRequest update, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
    Task DeleteAsync(Guid[] ids, CancellationToken ct);
}