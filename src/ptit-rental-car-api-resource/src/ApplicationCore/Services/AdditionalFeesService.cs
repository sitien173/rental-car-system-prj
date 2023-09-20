using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NGOT.ApplicationCore.Dto.AdditionalFee;
using NGOT.ApplicationCore.Entities;
using NGOT.ApplicationCore.Interfaces;

namespace NGOT.ApplicationCore.Services;

public class AdditionalFeesService : IAdditionalFeesService
{
    private readonly DbSet<AdditionalFees> _additionalFeesRepository;
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;

    public AdditionalFeesService(DbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _additionalFeesRepository = _dbContext.Set<AdditionalFees>();
    }

    public async Task<Guid> CreateAsync(CreateAdditionalFeesRequest request, CancellationToken ct = default)
    {
        var additionalFees = _mapper.Map<AdditionalFees>(request);

        await _additionalFeesRepository.AddAsync(additionalFees, ct);
        await _dbContext.SaveChangesAsync(ct);

        return additionalFees.Id;
    }

    public async Task<AdditionalFeesResponse?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var result = await _additionalFeesRepository
            .AsNoTracking()
            .ProjectTo<AdditionalFeesResponse>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(x => x.Id == id, ct);

        return Guard.Against.NotFound(id, result, nameof(result));
    }

    public Task<List<AdditionalFeesResponse>> GetAllAsync(CancellationToken ct = default)
    {
        return _additionalFeesRepository
            .AsNoTracking()
            .ProjectTo<AdditionalFeesResponse>(_mapper.ConfigurationProvider)
            .ToListAsync(ct);
    }

    public async Task<AdditionalFeesResponse> UpdateAsync(Guid id, UpdateAdditionalFeesRequest update,
        CancellationToken ct)
    {
        var additionalFees = await _additionalFeesRepository.FindAsync(new object[] { id }, ct);
        Guard.Against.NotFound(id, additionalFees, nameof(additionalFees));

        _mapper.Map(update, additionalFees);

        await _dbContext.SaveChangesAsync(ct);
        return _mapper.Map<AdditionalFeesResponse>(additionalFees);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var additionalFees = await _additionalFeesRepository.FindAsync(new object[] { id }, ct);
        Guard.Against.NotFound(id, additionalFees, nameof(additionalFees));

        _additionalFeesRepository.Remove(additionalFees);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid[] ids, CancellationToken ct)
    {
        var additionalFees = await _additionalFeesRepository.Where(x => ids.Contains(x.Id)).ToListAsync(ct);
        _additionalFeesRepository.RemoveRange(additionalFees);
        await _dbContext.SaveChangesAsync(ct);
    }
}