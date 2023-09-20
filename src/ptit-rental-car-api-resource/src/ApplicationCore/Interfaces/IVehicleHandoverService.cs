using NGOT.ApplicationCore.Dto.VehicleHandover;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Interfaces;

public interface IVehicleHandoverService : IScoped
{
    Task<Guid> CreateAsync(CreateVehicleHandoverRequest request, CancellationToken ct = default);
    Task<VehicleHandoverResponse?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<VehicleHandoverResponse>> GetAllAsync(CancellationToken ct = default);
    Task<VehicleHandoverResponse> UpdateAsync(Guid id, UpdateVehicleHandoverRequest update, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
    Task DeleteAsync(Guid[] ids, CancellationToken ct);
}