using Microsoft.EntityFrameworkCore;
using NGOT.ApplicationCore.Entities;
using NGOT.Common.Enums;
using NGOT.Infrastructure.Extensions;

namespace NGOT.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<AdditionalFees> AdditionalFees { get; set; } = null!;
    public DbSet<Brand> Brands { get; set; } = null!;
    public DbSet<Car> Cars { get; set; } = null!;
    public DbSet<CarAdditionalFees> CarAdditionalFees { get; set; } = null!;
    public DbSet<CarFeature> CarFeatures { get; set; } = null!;
    public DbSet<CarImage> CarImages { get; set; } = null!;
    public DbSet<CarRentalDocuments> CarRentalDocuments { get; set; } = null!;
    public DbSet<CarType> CarTypes { get; set; } = null!;
    public DbSet<CheckListItem> CheckListItems { get; set; } = null!;
    public DbSet<DamageAssessment> DamageAssessments { get; set; } = null!;
    public DbSet<Feature> Features { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<RentalContract> RentalContracts { get; set; } = null!;
    public DbSet<RentalDocuments> RentalDocuments { get; set; } = null!;
    public DbSet<RentalRequest> RentalRequests { get; set; } = null!;
    public DbSet<VehicleHandover> VehicleHandovers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // This ensures that EF Core accesses enum properties using their underlying fields.
        builder.UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.ApplyEnumToStringConversion();

        builder.ApplyConfigurationsFromAssembly(typeof(GlobalUsing).Assembly);
        base.OnModelCreating(builder);
    }
}