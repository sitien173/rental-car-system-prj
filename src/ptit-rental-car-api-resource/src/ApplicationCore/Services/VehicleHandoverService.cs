using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NGOT.ApplicationCore.Dto.VehicleHandover;
using NGOT.ApplicationCore.Entities;
using NGOT.ApplicationCore.Interfaces;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Services;

public class VehicleHandoverService : IVehicleHandoverService
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly DbSet<VehicleHandover> _vehicleHandoversRepository;

    public VehicleHandoverService(IMapper mapper, DbContext dbContext, ICurrentUserService currentUserService,
        IDateTimeProvider dateTimeProvider)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _currentUserService = currentUserService;
        _dateTimeProvider = dateTimeProvider;
        _vehicleHandoversRepository = _dbContext.Set<VehicleHandover>();
    }

    public async Task<Guid> CreateAsync(CreateVehicleHandoverRequest request, CancellationToken ct = default)
    {
        var vehicleHandover = _mapper.Map<VehicleHandover>(request);
        vehicleHandover.HandoverBy =
            Guard.Against.NullOrEmpty(_currentUserService.UserId, nameof(_currentUserService.UserId));
        vehicleHandover.HandoverDate = _dateTimeProvider.Now;

        await _vehicleHandoversRepository.AddAsync(vehicleHandover, ct);
        await _dbContext.SaveChangesAsync(ct);
        return vehicleHandover.Id;
    }

    public async Task<VehicleHandoverResponse?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var result = await _vehicleHandoversRepository
            .AsNoTracking()
            .ProjectTo<VehicleHandoverResponse>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(x => x.Id == id, ct);

        return Guard.Against.NotFound(id, result, nameof(result));
    }

    public Task<List<VehicleHandoverResponse>> GetAllAsync(CancellationToken ct = default)
    {
        return _vehicleHandoversRepository
            .AsNoTracking()
            .ProjectTo<VehicleHandoverResponse>(_mapper.ConfigurationProvider)
            .ToListAsync(ct);
    }

    public async Task<VehicleHandoverResponse> UpdateAsync(Guid id, UpdateVehicleHandoverRequest update,
        CancellationToken ct)
    {
        var vehicleHandover = await _vehicleHandoversRepository.FindAsync(new object[] { id }, ct);
        Guard.Against.NotFound(id, vehicleHandover, nameof(vehicleHandover));

        await _dbContext.Entry(vehicleHandover).Collection(x => x.CheckListItems).LoadAsync(ct);
        vehicleHandover.CheckListItems.Clear();

        _mapper.Map(update, vehicleHandover);

        await _dbContext.SaveChangesAsync(ct);
        return _mapper.Map<VehicleHandoverResponse>(vehicleHandover);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var vehicleHandover = await _vehicleHandoversRepository.FindAsync(new object[] { id }, ct);
        Guard.Against.NotFound(id, vehicleHandover, nameof(vehicleHandover));
        _vehicleHandoversRepository.Remove(vehicleHandover);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid[] ids, CancellationToken ct)
    {
        var vehicleHandovers = await _vehicleHandoversRepository.Where(x => ids.Contains(x.Id)).ToListAsync(ct);
        _vehicleHandoversRepository.RemoveRange(vehicleHandovers);
        await _dbContext.SaveChangesAsync(ct);
    }
}