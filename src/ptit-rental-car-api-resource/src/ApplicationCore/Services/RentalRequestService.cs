using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NGOT.ApplicationCore.Dto.RentalRequest;
using NGOT.ApplicationCore.Entities;
using NGOT.ApplicationCore.Interfaces;

namespace NGOT.ApplicationCore.Services;

public class RentalRequestService : IRentalRequestService
{
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly DbSet<RentalRequest> _rentalRequestRepository;

    public RentalRequestService(IMapper mapper, DbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _rentalRequestRepository = _dbContext.Set<RentalRequest>();
    }

    public async Task<IEnumerable<RentalRequestResponse>> GetAllAsync(CancellationToken ct)
    {
        var result = await _rentalRequestRepository
            .AsNoTracking()
            .ProjectTo<RentalRequestResponse>(_mapper.ConfigurationProvider)
            .ToListAsync(ct);
        return result;
    }

    public async Task<RentalRequestResponse> UpdateAsync(string id, UpdateRentalRequest request, CancellationToken ct)
    {
        var rentalRequest = await _rentalRequestRepository.FindAsync(new object[] { id }, ct);
        Guard.Against.NotFound(id, rentalRequest, nameof(rentalRequest));

        _mapper.Map(request, rentalRequest);

        await _dbContext.SaveChangesAsync(ct);
        return _mapper.Map<RentalRequestResponse>(rentalRequest);
    }

    public async Task DeleteAsync(string id, CancellationToken ct)
    {
        var rentalRequest = await _rentalRequestRepository.FindAsync(new object[] { id }, ct);
        Guard.Against.NotFound(id, rentalRequest, nameof(rentalRequest));
        _rentalRequestRepository.Remove(rentalRequest);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(string[] ids, CancellationToken ct)
    {
        var rentalRequests = await _rentalRequestRepository.Where(x => ids.Contains(x.Id)).ToListAsync(ct);
        _rentalRequestRepository.RemoveRange(rentalRequests);
        await _dbContext.SaveChangesAsync(ct);
    }
}