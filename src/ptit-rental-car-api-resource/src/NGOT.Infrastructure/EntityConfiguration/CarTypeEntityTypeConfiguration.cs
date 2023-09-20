using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using NGOT.ApplicationCore.Entities;

namespace NGOT.Infrastructure.EntityConfiguration;

public class CarTypeEntityTypeConfiguration : IEntityTypeConfiguration<CarType>
{
    public void Configure(EntityTypeBuilder<CarType> builder)
    {
        builder.HasIndex(x => new
        {
            x.Name
        }).IsUnique();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasValueGenerator<SequentialGuidValueGenerator>();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(x => x.Cars)
            .WithOne(x => x.CarType)
            .HasForeignKey(x => x.CarTypeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}