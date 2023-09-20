using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGOT.ApplicationCore.Entities;

namespace NGOT.Infrastructure.EntityConfiguration;

public class CarAdditionalFeesEntityTypeConfiguration : IEntityTypeConfiguration<CarAdditionalFees>
{
    public void Configure(EntityTypeBuilder<CarAdditionalFees> builder)
    {
        builder.HasKey(x => new { x.CarId, x.AdditionalFeesId });

        builder.HasOne<Car>(x => x.Car)
            .WithMany(x => x.CarAdditionalFees)
            .HasForeignKey(x => x.CarId);

        builder.HasOne(x => x.AdditionalFees)
            .WithMany(x => x.CarAdditionalFees)
            .HasForeignKey(x => x.AdditionalFeesId);
    }
}