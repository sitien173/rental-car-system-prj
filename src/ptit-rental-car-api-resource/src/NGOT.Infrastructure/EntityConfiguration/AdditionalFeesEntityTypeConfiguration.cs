using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGOT.ApplicationCore.Entities;

namespace NGOT.Infrastructure.EntityConfiguration;

public class AdditionalFeesEntityTypeConfiguration : IEntityTypeConfiguration<AdditionalFees>
{
    public void Configure(EntityTypeBuilder<AdditionalFees> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.Price)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.HasMany(x => x.CarAdditionalFees)
            .WithOne(x => x.AdditionalFees)
            .HasForeignKey(x => x.AdditionalFeesId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}