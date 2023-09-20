using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGOT.ApplicationCore.Entities;
using NGOT.Infrastructure.Generator;

namespace NGOT.Infrastructure.EntityConfiguration;

public class RentalContractEntityTypeConfiguration : IEntityTypeConfiguration<RentalContract>
{
    public void Configure(EntityTypeBuilder<RentalContract> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasValueGenerator<RentalContractIDGenerator>();

        builder.Property(rc => rc.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(rc => rc.CreatedBy)
            .IsRequired();

        builder.Property(rc => rc.PrepaidAmount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(rc => rc.Status)
            .IsRequired();

        // Invoices collection navigation property
        builder.HasMany(rc => rc.Payments)
            .WithOne(x => x.RentalContract)
            .HasForeignKey(i => i.RentalContractId)
            .OnDelete(DeleteBehavior.NoAction);

        // Vehicle handovers collection navigation property
        builder.HasMany(rc => rc.VehicleHandovers)
            .WithOne(vh => vh.RentalContract)
            .HasForeignKey(vh => vh.RentalContractId)
            .OnDelete(DeleteBehavior.NoAction);

        // Damage assessments collection navigation property
        builder.HasMany(rc => rc.DamageAssessments)
            .WithOne(da => da.RentalContract)
            .HasForeignKey(da => da.RentalContractId)
            .OnDelete(DeleteBehavior.NoAction);

        // Relationships
        builder.HasOne(x => x.RentalRequest)
            .WithOne(x => x.RentalContract)
            .HasForeignKey<RentalContract>(x => x.RentalRequestId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}