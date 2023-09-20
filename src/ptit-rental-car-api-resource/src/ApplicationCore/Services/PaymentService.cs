using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NGOT.ApplicationCore.Dto.Payment;
using NGOT.ApplicationCore.Entities;
using NGOT.ApplicationCore.Interfaces;
using NGOT.Common.Enums;

namespace NGOT.ApplicationCore.Services;

public class PaymentService : IPaymentService
{
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly DbSet<Payment> _paymentRepository;

    public PaymentService(IMapper mapper, DbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _paymentRepository = _dbContext.Set<Payment>();
    }

    public async Task<Guid> CreateAsync(CreatePaymentRequest request, CancellationToken ct = default)
    {
        var payment = _mapper.Map<Payment>(request);
        payment.Status = PaymentStatusEnum.Completed;
        await _paymentRepository.AddAsync(payment, ct);
        await _dbContext.SaveChangesAsync(ct);
        return payment.Id;
    }

    public async Task<PaymentResponse?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var result = await _paymentRepository
            .AsNoTracking()
            .ProjectTo<PaymentResponse>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(x => x.Id == id, ct);

        return Guard.Against.NotFound(id, result, nameof(result));
    }

    public Task<List<PaymentResponse>> GetAllAsync(CancellationToken ct = default)
    {
        return _paymentRepository
            .AsNoTracking()
            .ProjectTo<PaymentResponse>(_mapper.ConfigurationProvider)
            .ToListAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var payment = await _paymentRepository.FindAsync(new object[] { id }, ct);
        Guard.Against.NotFound(id, payment, nameof(payment));
        _paymentRepository.Remove(payment);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid[] ids, CancellationToken ct)
    {
        var payments = await _paymentRepository.Where(x => ids.Contains(x.Id)).ToListAsync(ct);
        _paymentRepository.RemoveRange(payments);
        await _dbContext.SaveChangesAsync(ct);
    }
}