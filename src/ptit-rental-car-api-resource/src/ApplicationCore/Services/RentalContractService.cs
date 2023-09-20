using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NGOT.ApplicationCore.Dto.RentalContract;
using NGOT.ApplicationCore.Entities;
using NGOT.ApplicationCore.Interfaces;
using NGOT.Common.Enums;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore.Services;

public class RentalContractService : IRentalContractService
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly DbSet<RentalContract> _rentalContractsRepository;
    private readonly DbSet<Car> _carsRepository;

    public RentalContractService(IMapper mapper, DbContext dbContext, ICurrentUserService currentUserService,
        IDateTimeProvider dateTimeProvider)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _currentUserService = currentUserService;
        _dateTimeProvider = dateTimeProvider;
        _rentalContractsRepository = _dbContext.Set<RentalContract>();
        _carsRepository = _dbContext.Set<Car>();
    }

    public async Task<string> CreateAsync(CreateRentalContractRequest request, CancellationToken ct = default)
    {
        var rentalContract = _mapper.Map<RentalContract>(request);
        rentalContract.CreatedBy = _currentUserService.UserId;
        rentalContract.Status = RentalContractStatusEnum.Active;
        rentalContract.Created = _dateTimeProvider.Now;
        await _rentalContractsRepository.AddAsync(rentalContract, ct);
        await _dbContext.SaveChangesAsync(ct);
        return rentalContract.Id;
    }

    public async Task<IEnumerable<RentalContractResponse>> GetAllAsync(CancellationToken ct = default)
    {
        var result = await _rentalContractsRepository
            .AsNoTracking()
            .ProjectTo<RentalContractResponse>(_mapper.ConfigurationProvider)
            .ToListAsync(ct);
        return result;
    }

    public async Task<RentalContractResponse> UpdateAsync(string id, UpdateRentalContractRequest update,
        CancellationToken ct)
    {
        var rentalContract = await _rentalContractsRepository.FindAsync(new object[] { id }, ct);
        Guard.Against.NotFound(id, rentalContract, nameof(rentalContract));

        _mapper.Map(update, rentalContract);
        rentalContract.LastModifiedBy = _currentUserService.UserId;
        rentalContract.LastModified = _dateTimeProvider.Now;
        
        var car = await _carsRepository.FindAsync(new object[] { rentalContract.RentalRequest.CarId }, ct);
        Guard.Against.NotFound(rentalContract.RentalRequest.CarId, car, nameof(car));
        
        car.Status = rentalContract.Status == RentalContractStatusEnum.Active ? CarStatusEnum.Rented : CarStatusEnum.Available;

        await _dbContext.SaveChangesAsync(ct);
        return _mapper.Map<RentalContractResponse>(rentalContract);
    }

    public async Task DeleteAsync(string id, CancellationToken ct)
    {
        var rentalContract = await _rentalContractsRepository.FindAsync(new object[] { id }, ct);
        Guard.Against.NotFound(id, rentalContract, nameof(rentalContract));
        _rentalContractsRepository.Remove(rentalContract);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(string[] ids, CancellationToken ct)
    {
        var rentalContracts = await _rentalContractsRepository.Where(x => ids.Contains(x.Id)).ToListAsync(ct);
        _rentalContractsRepository.RemoveRange(rentalContracts);
        await _dbContext.SaveChangesAsync(ct);
    }
}