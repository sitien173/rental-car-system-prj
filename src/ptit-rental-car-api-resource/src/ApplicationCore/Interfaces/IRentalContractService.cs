using NGOT.ApplicationCore.Dto.RentalContract;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Interfaces;

public interface IRentalContractService : IScoped
{
    Task<string> CreateAsync(CreateRentalContractRequest request, CancellationToken ct);
    Task<IEnumerable<RentalContractResponse>> GetAllAsync(CancellationToken ct);
    Task<RentalContractResponse> UpdateAsync(string id, UpdateRentalContractRequest request, CancellationToken ct);
    Task DeleteAsync(string id, CancellationToken ct);
    Task DeleteAsync(string[] ids, CancellationToken ct);
}