using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using NGOT.ApplicationCore.Entities;

namespace NGOT.Infrastructure.EntityConfiguration;

public class DamageAssessmentEntityTypeConfiguration : IEntityTypeConfiguration<DamageAssessment>
{
    public void Configure(EntityTypeBuilder<DamageAssessment> builder)
    {
        builder.HasKey(da => da.Id);

        builder.Property(da => da.Id)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<SequentialGuidValueGenerator>();

        builder.Property(da => da.AssessmentDate)
            .IsRequired();

        builder.Property(da => da.DamageDescription)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(da => da.RepairCost)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(da => da.TotalCost)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(da => da.RentalContractId)
            .IsRequired();

        builder.HasOne(da => da.RentalContract)
            .WithMany(ra => ra.DamageAssessments)
            .HasForeignKey(da => da.RentalContractId);
    }
}