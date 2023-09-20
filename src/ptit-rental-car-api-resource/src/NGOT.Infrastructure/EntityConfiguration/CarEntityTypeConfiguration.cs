using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using NGOT.ApplicationCore.Entities;

namespace NGOT.Infrastructure.EntityConfiguration;

public class CarEntityTypeConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasValueGenerator<SequentialGuidValueGenerator>();

        // Properties
        builder.Property(c => c.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasMaxLength(2000);

        builder.Property(c => c.Status)
            .IsRequired();

        builder.Property(c => c.Price)
            .IsRequired()
            .HasPrecision(18, 2);

        // Relationships
        builder.HasOne(c => c.Brand)
            .WithMany(b => b.Cars)
            .HasForeignKey(c => c.BrandId);

        // CarImages collection navigation property
        builder.HasMany(c => c.CarImages)
            .WithOne(i => i.Car)
            .HasForeignKey(ci => ci.CarId)
            .OnDelete(DeleteBehavior.Cascade);

        // CarFeatures collection navigation property
        builder.HasMany(c => c.CarFeatures)
            .WithOne(f => f.Car)
            .HasForeignKey(cf => cf.CarId)
            .OnDelete(DeleteBehavior.Cascade);

        // CarAdditionalFees collection navigation property
        builder.HasMany(c => c.CarAdditionalFees)
            .WithOne(f => f.Car)
            .HasForeignKey(cf => cf.CarId)
            .OnDelete(DeleteBehavior.Cascade);

        // CarRentalDocuments collection navigation property
        builder.HasMany(c => c.CarRentalDocuments)
            .WithOne(f => f.Car)
            .HasForeignKey(cf => cf.CarId)
            .OnDelete(DeleteBehavior.Cascade);

        // RentalRequests collection navigation property
        builder.HasMany(c => c.RentalRequests)
            .WithOne(f => f.Car)
            .HasForeignKey(cf => cf.CarId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}