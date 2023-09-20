using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGOT.ApplicationCore.Entities;

namespace NGOT.Infrastructure.EntityConfiguration;

public class CarFeatureEntityTypeConfiguration : IEntityTypeConfiguration<CarFeature>
{
    public void Configure(EntityTypeBuilder<CarFeature> builder)
    {
        builder.HasKey(x => new { x.CarId, x.FeatureId });
        builder.Property(x => x.CarId).IsRequired();
        builder.Property(x => x.FeatureId).IsRequired();

        builder.HasOne(x => x.Car)
            .WithMany(x => x.CarFeatures)
            .HasForeignKey(x => x.CarId);

        builder.HasOne(x => x.Feature)
            .WithMany(x => x.CarFeatures)
            .HasForeignKey(x => x.FeatureId);
    }
}