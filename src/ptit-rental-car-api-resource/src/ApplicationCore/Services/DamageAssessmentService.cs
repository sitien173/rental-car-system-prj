using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NGOT.ApplicationCore.Dto.DamageAssessment;
using NGOT.ApplicationCore.Entities;
using NGOT.ApplicationCore.Interfaces;

namespace NGOT.ApplicationCore.Services;

public class DamageAssessmentService : IDamageAssessmentService
{
    private readonly DbSet<DamageAssessment> _damageAssessmentsRepository;
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;

    public DamageAssessmentService(IMapper mapper, DbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _damageAssessmentsRepository = _dbContext.Set<DamageAssessment>();
    }

    public async Task<Guid> CreateAsync(CreateDamageAssessmentRequest request, CancellationToken ct = default)
    {
        var damageAssessment = _mapper.Map<DamageAssessment>(request);
        await _damageAssessmentsRepository.AddAsync(damageAssessment, ct);
        await _dbContext.SaveChangesAsync(ct);
        return damageAssessment.Id;
    }

    public async Task<DamageAssessmentResponse?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var result = await _damageAssessmentsRepository
            .AsNoTracking()
            .ProjectTo<DamageAssessmentResponse>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(x => x.Id == id, ct);

        return Guard.Against.NotFound(id, result, nameof(result));
    }

    public Task<List<DamageAssessmentResponse>> GetAllAsync(CancellationToken ct = default)
    {
        return _damageAssessmentsRepository
            .AsNoTracking()
            .ProjectTo<DamageAssessmentResponse>(_mapper.ConfigurationProvider)
            .ToListAsync(ct);
    }

    public async Task<DamageAssessmentResponse> UpdateAsync(Guid id, UpdateDamageAssessmentRequest update,
        CancellationToken ct)
    {
        var damageAssessment = await _damageAssessmentsRepository.FindAsync(new object[] { id }, ct);
        Guard.Against.NotFound(id, damageAssessment, nameof(damageAssessment));
        _mapper.Map(update, damageAssessment);

        await _dbContext.SaveChangesAsync(ct);
        return _mapper.Map<DamageAssessmentResponse>(damageAssessment);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var damageAssessment = await _damageAssessmentsRepository.FindAsync(new object[] { id }, ct);
        Guard.Against.NotFound(id, damageAssessment, nameof(damageAssessment));
        _damageAssessmentsRepository.Remove(damageAssessment);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid[] ids, CancellationToken ct)
    {
        var damageAssessments = await _damageAssessmentsRepository.Where(x => ids.Contains(x.Id)).ToListAsync(ct);
        _damageAssessmentsRepository.RemoveRange(damageAssessments);
        await _dbContext.SaveChangesAsync(ct);
    }
}