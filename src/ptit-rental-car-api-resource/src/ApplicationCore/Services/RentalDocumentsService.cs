using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NGOT.ApplicationCore.Dto.RentalDocuments;
using NGOT.ApplicationCore.Entities;
using NGOT.ApplicationCore.Interfaces;
using NGOT.Common.Extensions;

namespace NGOT.ApplicationCore.Services;

public class RentalDocumentsService : IRentalDocumentsService
{
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly DbSet<RentalDocuments> _rentalDocumentsRepository;

    public RentalDocumentsService(IMapper mapper, DbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _rentalDocumentsRepository = _dbContext.Set<RentalDocuments>();
    }

    public async Task<Guid> CreateAsync(CreateRentalDocumentsRequest request, CancellationToken ct = default)
    {
        var existingRentalDocuments = await _rentalDocumentsRepository.AnyAsync(x => x.Name == request.Name, ct);
        Guard.Against.False(existingRentalDocuments, nameof(request.Name),
            $"RentalDocuments with name {request.Name} already exists");

        var rentalDocuments = _mapper.Map<RentalDocuments>(request);
        await _rentalDocumentsRepository.AddAsync(rentalDocuments, ct);
        await _dbContext.SaveChangesAsync(ct);
        return rentalDocuments.Id;
    }

    public async Task<RentalDocumentsResponse?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var result = await _rentalDocumentsRepository
            .AsNoTracking()
            .ProjectTo<RentalDocumentsResponse>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(x => x.Id == id, ct);

        return Guard.Against.NotFound(id, result, nameof(result));
    }

    public Task<List<RentalDocumentsResponse>> GetAllAsync(CancellationToken ct = default)
    {
        return _rentalDocumentsRepository
            .AsNoTracking()
            .ProjectTo<RentalDocumentsResponse>(_mapper.ConfigurationProvider)
            .ToListAsync(ct);
    }

    public async Task<RentalDocumentsResponse> UpdateAsync(Guid id, UpdateRentalDocumentsRequest update,
        CancellationToken ct)
    {
        var rentalDocuments = await _rentalDocumentsRepository.FindAsync(new object[] { id }, ct);
        Guard.Against.NotFound(id, rentalDocuments, nameof(rentalDocuments));

        var existingRentalDocuments =
            await _rentalDocumentsRepository.AnyAsync(x => x.Name == update.Name && x.Id != id, ct);
        Guard.Against.False(existingRentalDocuments, nameof(update.Name),
            $"RentalDocuments with name {update.Name} already exists");
        _mapper.Map(update, rentalDocuments);

        await _dbContext.SaveChangesAsync(ct);
        return _mapper.Map<RentalDocumentsResponse>(rentalDocuments);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var rentalDocuments = await _rentalDocumentsRepository.FindAsync(new object[] { id }, ct);
        Guard.Against.NotFound(id, rentalDocuments, nameof(rentalDocuments));
        _rentalDocumentsRepository.Remove(rentalDocuments);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid[] ids, CancellationToken ct)
    {
        var rentalDocuments = await _rentalDocumentsRepository.Where(x => ids.Contains(x.Id)).ToListAsync(ct);
        _rentalDocumentsRepository.RemoveRange(rentalDocuments);
        await _dbContext.SaveChangesAsync(ct);
    }
}