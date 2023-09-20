using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGOT.ApplicationCore.Entities;
using NGOT.Infrastructure.Generator;

namespace NGOT.Infrastructure.EntityConfiguration;

public class RentalRequestEntityTypeConfiguration : IEntityTypeConfiguration<RentalRequest>
{
    public void Configure(EntityTypeBuilder<RentalRequest> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasValueGenerator<RentalRequestGenerator>();

        // Properties
        builder.Property(rr => rr.UserId)
            .IsRequired();

        builder.Property(x => x.CarId)
            .IsRequired();

        builder.Property(rr => rr.StartDate)
            .IsRequired();

        builder.Property(rr => rr.EndDate)
            .IsRequired();

        builder.Property(rr => rr.Status)
            .IsRequired();

        builder.HasOne<Car>(x => x.Car)
            .WithMany(x => x.RentalRequests)
            .HasForeignKey(r => r.CarId);

        builder.HasOne(x => x.RentalContract)
            .WithOne(x => x.RentalRequest)
            .HasForeignKey<RentalContract>(x => x.RentalRequestId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}