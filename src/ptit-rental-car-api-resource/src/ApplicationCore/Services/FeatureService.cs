using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NGOT.ApplicationCore.Dto.Feature;
using NGOT.ApplicationCore.Entities;
using NGOT.ApplicationCore.Interfaces;
using NGOT.Common.Extensions;

namespace NGOT.ApplicationCore.Services;

public class FeatureService : IFeatureService
{
    private readonly DbContext _dbContext;
    private readonly DbSet<Feature> _featureRepository;
    private readonly IMapper _mapper;

    public FeatureService(IMapper mapper, DbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _featureRepository = _dbContext.Set<Feature>();
    }

    public async Task<Guid> CreateAsync(CreateFeatureRequest request, CancellationToken ct = default)
    {
        // check index duplication
        var existingFeature = await _featureRepository.AnyAsync(x => x.Name == request.Name, ct);
        Guard.Against.False(existingFeature, nameof(request.Name), $"Feature with name {request.Name} already exists");

        var feature = _mapper.Map<Feature>(request);
        await _featureRepository.AddAsync(feature, ct);
        await _dbContext.SaveChangesAsync(ct);
        return feature.Id;
    }

    public async Task<FeatureResponse?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var result = await _featureRepository
            .AsNoTracking()
            .ProjectTo<FeatureResponse>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(x => x.Id == id, ct);

        return Guard.Against.NotFound(id, result, nameof(result));
    }

    public Task<List<FeatureResponse>> GetAllAsync(CancellationToken ct = default)
    {
        return _featureRepository
            .AsNoTracking()
            .ProjectTo<FeatureResponse>(_mapper.ConfigurationProvider)
            .ToListAsync(ct);
    }

    public async Task<FeatureResponse> UpdateAsync(Guid id, UpdateFeatureRequest update, CancellationToken ct)
    {
        var feature = await _featureRepository.FindAsync(new object[] { id }, ct);
        Guard.Against.NotFound(id, feature, nameof(feature));

        var existingFeature = await _featureRepository.AnyAsync(x => x.Name == update.Name && x.Id != id, ct);
        Guard.Against.False(existingFeature, nameof(update.Name), $"Feature with name {update.Name} already exists");
        _mapper.Map(update, feature);

        await _dbContext.SaveChangesAsync(ct);
        return _mapper.Map<FeatureResponse>(feature);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var feature = await _featureRepository.FindAsync(new object[] { id }, ct);
        Guard.Against.NotFound(id, feature, nameof(feature));
        _featureRepository.Remove(feature);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid[] ids, CancellationToken ct)
    {
        var features = await _featureRepository.Where(x => ids.Contains(x.Id)).ToListAsync(ct);
        _featureRepository.RemoveRange(features);
        await _dbContext.SaveChangesAsync(ct);
    }
}