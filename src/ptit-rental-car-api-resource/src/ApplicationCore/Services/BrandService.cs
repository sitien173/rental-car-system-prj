using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NGOT.ApplicationCore.Dto.Brand;
using NGOT.ApplicationCore.Entities;
using NGOT.ApplicationCore.Interfaces;
using NGOT.Common.Extensions;
using NGOT.Common.Models;
using NGOT.Common.Utils;

namespace NGOT.ApplicationCore.Services;

public class BrandService : IBrandService
{
    private readonly DbSet<Brand> _brandRepository;
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;

    public BrandService(IMapper mapper, DbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _brandRepository = _dbContext.Set<Brand>();
    }

    public Task<List<BrandResponse>> GetAllAsync(CancellationToken ct)
    {
        return _brandRepository
            .AsNoTracking()
            .ProjectTo<BrandResponse>(_mapper.ConfigurationProvider)
            .ToListAsync(ct);
    }

    public async Task<BrandResponse?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var result = await _brandRepository
            .AsNoTracking()
            .ProjectTo<BrandResponse>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(x => x.Id == id, ct);

        return Guard.Against.NotFound(id, result, nameof(result));
    }

    public async Task<Guid> CreateAsync(CreateBrandRequest request, CancellationToken ct)
    {
        Guard.Against.Null(request, nameof(request));

        // check if brand name already exists
        var existingName = await _brandRepository.AnyAsync(x => x.Name == request.Name, ct);

        Guard.Against.False(existingName, nameof(existingName), $"Brand name {request.Name} already exists");

        var brand = _mapper.Map<Brand>(request);

        await _brandRepository.AddAsync(brand, ct);
        await _dbContext.SaveChangesAsync(ct);

        return brand.Id;
    }

    public async Task<BrandResponse> UpdateAsync(Guid id, UpdateBrandRequest update, CancellationToken ct)
    {
        Guard.Against.Null(update, nameof(update));

        var brand = await _brandRepository.FindAsync(new object[] { id }, ct);

        Guard.Against.NotFound(id, brand, nameof(brand));

        // check if brand name already exists
        var existingName = await _brandRepository.AnyAsync(x => x.Name == update.Name && x.Id != id, ct);
        Guard.Against.False(existingName, nameof(update.Name), $"Brand name {update.Name} already exists");

        _mapper.Map(update, brand);

        await _dbContext.SaveChangesAsync(ct);
        return _mapper.Map<BrandResponse>(brand);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var brand = await _brandRepository.FindAsync(new object[] { id }, ct);
        Guard.Against.NotFound(id, brand, nameof(brand));
        _brandRepository.Remove(brand);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid[] ids, CancellationToken ct)
    {
        var brands = await _brandRepository.Where(x => ids.Contains(x.Id)).ToListAsync(ct);

        _brandRepository.RemoveRange(brands);
        await _dbContext.SaveChangesAsync(ct);
    }
}