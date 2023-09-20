using NGOT.ApplicationCore.Dto.Car;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Interfaces;

public interface ICarService : IScoped
{
    Task<List<CarResponse>> GetAllAsync(CancellationToken ct = default);
    Task<CarResponse?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Guid> CreateAsync(CreateCarRequest request, CancellationToken ct = default);
    Task<CarResponse> UpdateAsync(Guid id, UpdateCarRequest request, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
    Task DeleteAsync(Guid[] ids, CancellationToken ct);
    Task<bool> RentAsync(Guid id, RentCarRequest request, CancellationToken ct);
}