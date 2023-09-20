using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NGOT.ApplicationCore.Dto.CarType;
using NGOT.ApplicationCore.Entities;
using NGOT.ApplicationCore.Interfaces;

namespace NGOT.ApplicationCore.Services;

public class CarTypeService : ICarTypeService
{
    private readonly DbSet<CarType> _carTypeRepository;
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;

    public CarTypeService(IMapper mapper, DbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _carTypeRepository = _dbContext.Set<CarType>();
    }

    public async Task<Guid> CreateAsync(CreateCarTypeRequest request, CancellationToken ct = default)
    {
        Guard.Against.Null(request, nameof(request));

        var carType = _mapper.Map<CarType>(request);

        await _carTypeRepository.AddAsync(carType, ct);
        await _dbContext.SaveChangesAsync(ct);
        return carType.Id;
    }

    public async Task<CarTypeResponse?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var result = await _carTypeRepository
            .AsNoTracking()
            .ProjectTo<CarTypeResponse>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(x => x.Id == id, ct);

        return Guard.Against.NotFound(id, result, nameof(result));
    }

    public Task<List<CarTypeResponse>> GetAllAsync(CancellationToken ct = default)
    {
        return _carTypeRepository
            .AsNoTracking()
            .ProjectTo<CarTypeResponse>(_mapper.ConfigurationProvider)
            .ToListAsync(ct);
    }

    public async Task<CarTypeResponse> UpdateAsync(Guid id, UpdateCarTypeRequest request, CancellationToken ct)
    {
        var carType = await _carTypeRepository.FindAsync(new object[] { id }, ct);
        Guard.Against.NotFound(id, carType, nameof(carType));

        _mapper.Map(request, carType);
        await _dbContext.SaveChangesAsync(ct);
        return _mapper.Map<CarTypeResponse>(carType);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var carType = await _carTypeRepository.FindAsync(new object[] { id }, ct);
        Guard.Against.NotFound(id, carType, nameof(carType));

        _carTypeRepository.Remove(carType);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid[] ids, CancellationToken ct)
    {
        var carTypes = await _carTypeRepository.Where(x => ids.Contains(x.Id)).ToListAsync(ct);
        _carTypeRepository.RemoveRange(carTypes);
        await _dbContext.SaveChangesAsync(ct);
    }
}