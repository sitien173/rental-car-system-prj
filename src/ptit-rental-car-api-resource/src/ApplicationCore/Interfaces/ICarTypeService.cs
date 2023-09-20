using NGOT.ApplicationCore.Dto.CarType;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Interfaces;

public interface ICarTypeService : IScoped
{
    Task<Guid> CreateAsync(CreateCarTypeRequest request, CancellationToken ct = default);
    Task<CarTypeResponse?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<CarTypeResponse>> GetAllAsync(CancellationToken ct = default);
    Task<CarTypeResponse> UpdateAsync(Guid id, UpdateCarTypeRequest request, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
    Task DeleteAsync(Guid[] ids, CancellationToken ct);
}