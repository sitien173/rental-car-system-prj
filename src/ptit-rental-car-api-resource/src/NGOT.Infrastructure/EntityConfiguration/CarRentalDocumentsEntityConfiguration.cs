using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGOT.ApplicationCore.Entities;

namespace NGOT.Infrastructure.EntityConfiguration;

public class CarRentalDocumentsEntityConfiguration : IEntityTypeConfiguration<CarRentalDocuments>
{
    public void Configure(EntityTypeBuilder<CarRentalDocuments> builder)
    {
        // Keys
        builder.HasKey(x => new { x.CarId, x.RentalDocumentId });

        // Relationships
        builder.HasOne(x => x.Car)
            .WithMany(x => x.CarRentalDocuments)
            .HasForeignKey(x => x.CarId);

        builder.HasOne(x => x.RentalDocuments)
            .WithMany(x => x.CarRentalDocuments)
            .HasForeignKey(x => x.RentalDocumentId);
    }
}