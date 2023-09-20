using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using NGOT.ApplicationCore.Entities;

namespace NGOT.Infrastructure.EntityConfiguration;

public class PaymentEntityTypeConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasValueGenerator<SequentialGuidValueGenerator>();

        // Properties
        builder.Property(p => p.PaymentDate)
            .IsRequired();

        builder.Property(p => p.Amount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(p => p.Status)
            .IsRequired();

        // Relationships
        builder.HasOne<RentalContract>(x => x.RentalContract)
            .WithMany(x => x.Payments)
            .HasForeignKey(p => p.RentalContractId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}