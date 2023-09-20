using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using NGOT.ApplicationCore.Entities;

namespace NGOT.Infrastructure.EntityConfiguration;

public class RentalDocumentsEntityTypeConfiguration : IEntityTypeConfiguration<RentalDocuments>
{
    public void Configure(EntityTypeBuilder<RentalDocuments> builder)
    {
        builder.HasIndex(x => new
        {
            x.Name
        }).IsUnique();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasValueGenerator<SequentialGuidValueGenerator>();

        // Properties
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        // Relationship
        builder.HasMany(x => x.CarRentalDocuments)
            .WithOne(x => x.RentalDocuments)
            .HasForeignKey(x => x.RentalDocumentId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}