using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using NGOT.ApplicationCore.Entities;

namespace NGOT.Infrastructure.EntityConfiguration;

public class VehicleHandoverEntityTypeConfiguration : IEntityTypeConfiguration<VehicleHandover>
{
    public void Configure(EntityTypeBuilder<VehicleHandover> builder)
    {
        builder.HasKey(vh => vh.Id);

        builder.Property(vh => vh.Id)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<SequentialGuidValueGenerator>();

        builder.Property(vh => vh.HandoverDate)
            .IsRequired();

        builder.Property(vh => vh.HandoverBy)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(vh => vh.HandoverType)
            .IsRequired();

        builder.Property(vh => vh.RentalContractId)
            .IsRequired();

        builder.HasOne(vh => vh.RentalContract)
            .WithMany(rc => rc.VehicleHandovers)
            .HasForeignKey(vh => vh.RentalContractId);

        builder.HasMany(vh => vh.CheckListItems)
            .WithOne(cli => cli.VehicleHandover)
            .HasForeignKey(cli => cli.VehicleHandoverId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}