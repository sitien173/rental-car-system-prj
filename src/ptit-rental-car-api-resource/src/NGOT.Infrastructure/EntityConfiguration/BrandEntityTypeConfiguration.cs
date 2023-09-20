using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using NGOT.ApplicationCore.Entities;

namespace NGOT.Infrastructure.EntityConfiguration;

public class BrandEntityTypeConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.HasIndex(x => new
        {
            x.Name
        }).IsUnique();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasValueGenerator<SequentialGuidValueGenerator>();

        // Properties
        builder.Property(b => b.Name)
            .HasMaxLength(30)
            .IsRequired();

        // Relationships
        builder.HasMany(b => b.Cars)
            .WithOne(c => c.Brand)
            .HasForeignKey(c => c.BrandId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}