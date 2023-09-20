using NGOT.ApplicationCore.Dto.Payment;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Interfaces;

public interface IPaymentService : IScoped
{
    Task<Guid> CreateAsync(CreatePaymentRequest request, CancellationToken ct = default);
    Task<PaymentResponse?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<PaymentResponse>> GetAllAsync(CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct);
    Task DeleteAsync(Guid[] ids, CancellationToken ct);
}