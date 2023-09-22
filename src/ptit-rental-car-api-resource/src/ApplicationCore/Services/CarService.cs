using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NGOT.ApplicationCore.Dto.Car;
using NGOT.ApplicationCore.Entities;
using NGOT.ApplicationCore.Interfaces;
using NGOT.Common.Enums;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Services;

public class CarService : ICarService
{
    private readonly DbSet<Car> _carsRepository;
    private readonly DbSet<RentalContract> _rentalContractsRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;

    public CarService(IMapper mapper, DbContext dbContext, ICurrentUserService currentUserService)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _currentUserService = currentUserService;
        _carsRepository = dbContext.Set<Car>();
        _rentalContractsRepository = dbContext.Set<RentalContract>();
    }

    public Task<List<CarResponse>> GetAllAsync(CancellationToken ct = default)
    {
        return _carsRepository
            .AsNoTracking()
            .ProjectTo<CarResponse>(_mapper.ConfigurationProvider)
            .ToListAsync(ct);
    }

    public Task<CarResponse?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return _carsRepository
            .AsNoTracking()
            .ProjectTo<CarResponse>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<Guid> CreateAsync(CreateCarRequest request, CancellationToken ct = default)
    {
        var car = _mapper.Map<Car>(request);
        await _carsRepository.AddAsync(car, ct);
        await _dbContext.SaveChangesAsync(ct);
        return car.Id;
    }

    public async Task<CarResponse> UpdateAsync(Guid id, UpdateCarRequest request, CancellationToken ct)
    {
        var car = await _carsRepository.FindAsync(new object[] { id }, ct);
        Guard.Against.NotFound(id, car, nameof(Car));

        _mapper.Map(request, car);
        if (request.FeatureIds.Any())
        {
            await _carsRepository.Attach(car).Collection(x => x.CarFeatures).LoadAsync(ct);
            car.CarFeatures.Clear();
            car.CarFeatures.AddRange(request.FeatureIds.Select(x => new CarFeature { FeatureId = x }));
        }

        if (request.AdditionalFeeIds.Any())
        {
            await _carsRepository.Attach(car).Collection(x => x.CarAdditionalFees).LoadAsync(ct);
            car.CarAdditionalFees.Clear();
            car.CarAdditionalFees.AddRange(request.AdditionalFeeIds.Select(x => new CarAdditionalFees
                { AdditionalFeesId = x }));
        }

        if (request.RentalDocumentIds.Any())
        {
            await _carsRepository.Attach(car).Collection(x => x.CarRentalDocuments).LoadAsync(ct);
            car.CarRentalDocuments.Clear();
            car.CarRentalDocuments.AddRange(request.RentalDocumentIds.Select(x => new CarRentalDocuments
                { RentalDocumentId = x }));
        }

        if (request.Images.Any())
        {
            await _carsRepository.Attach(car).Collection(x => x.CarImages).LoadAsync(ct);
            car.CarImages.Clear();
            car.CarImages.AddRange(request.Images.Select(x => new CarImage { Image = x }));
        }

        await _dbContext.SaveChangesAsync(ct);
        return _mapper.Map<CarResponse>(car);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var car = await _carsRepository.FindAsync(new object[] { id }, ct);
        Guard.Against.NotFound(id, car, nameof(Car));

        _carsRepository.Remove(car);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid[] ids, CancellationToken ct)
    {
        var cars = await _carsRepository
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(ct);

        _carsRepository.RemoveRange(cars);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<bool> RentAsync(Guid id, RentCarRequest request, CancellationToken ct)
    {
        var car = await _carsRepository.FindAsync(new object[] { id }, ct);
        Guard.Against.NotFound(id, car, nameof(Car));

        if (car.Status != CarStatusEnum.Available) 
            return false;
        
        bool canRent = _rentalContractsRepository
            .Where(x => x.RentalRequest.CarId == id)
            .All(x => x.RentalRequest.StartDate > request.EndDate || x.RentalRequest.EndDate < request.StartDate);

        if (!canRent)
            return false;
        
        car.Status = CarStatusEnum.Rented;
        car.RentalRequests.Add(new RentalRequest
        {
            CarId = car.Id,
            UserId = Guid.Parse(Guard.Against.NullOrEmpty(_currentUserService.UserId,
                nameof(_currentUserService.UserId))),
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Status = RentalRequestEnum.Pending
        });

        await _dbContext.SaveChangesAsync(ct);
        return true;
    }
}