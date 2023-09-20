using NGOT.ApplicationCore.Dto.DamageAssessment;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Interfaces;

public interface IDamageAssessmentService : IScoped
{
    Task<Guid> CreateAsync(CreateDamageAssessmentRequest request, CancellationToken ct = default);
    Task<DamageAssessmentResponse?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<DamageAssessmentResponse>> GetAllAsync(CancellationToken ct = default);
    Task<DamageAssessmentResponse> UpdateAsync(Guid id, UpdateDamageAssessmentRequest update, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
    Task DeleteAsync(Guid[] ids, CancellationToken ct);
}