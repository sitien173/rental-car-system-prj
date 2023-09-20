using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using NGOT.ApplicationCore.Entities;

namespace NGOT.Infrastructure.EntityConfiguration;

public class FeatureEntityTypeConfiguration : IEntityTypeConfiguration<Feature>
{
    public void Configure(EntityTypeBuilder<Feature> builder)
    {
        builder.HasIndex(x => new
        {
            x.Name
        }).IsUnique();

        // Primary key
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasValueGenerator<SequentialGuidValueGenerator>();

        // Properties
        builder.Property(x => x.Name)
            .HasMaxLength(255)
            .IsRequired();

        // Optional relationships
        builder.HasMany(x => x.CarFeatures)
            .WithOne(x => x.Feature)
            .HasForeignKey(x => x.FeatureId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}