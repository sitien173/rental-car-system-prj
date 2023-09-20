using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using NGOT.ApplicationCore.Entities;

namespace NGOT.Infrastructure.EntityConfiguration;

public class CarImageEntityTypeConfiguration : IEntityTypeConfiguration<CarImage>
{
    public void Configure(EntityTypeBuilder<CarImage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasValueGenerator<SequentialGuidValueGenerator>();

        // Properties
        builder.Property(x => x.CarId)
            .IsRequired();

        builder.Property(ci => ci.IsMain)
            .IsRequired();

        // Relationships
        builder.HasOne(x => x.Car)
            .WithMany(c => c.CarImages)
            .HasForeignKey(ci => ci.CarId);
    }
}