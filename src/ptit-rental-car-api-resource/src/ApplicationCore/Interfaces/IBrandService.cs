using NGOT.ApplicationCore.Dto.Brand;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Interfaces;

public interface IBrandService : IScoped
{
    Task<List<BrandResponse>> GetAllAsync(CancellationToken ct);
    Task<BrandResponse?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<Guid> CreateAsync(CreateBrandRequest request, CancellationToken ct);
    Task<BrandResponse> UpdateAsync(Guid id, UpdateBrandRequest request, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
    Task DeleteAsync(Guid[] ids, CancellationToken ct);
}