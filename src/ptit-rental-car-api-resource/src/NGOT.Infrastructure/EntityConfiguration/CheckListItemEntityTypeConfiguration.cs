using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGOT.ApplicationCore.Entities;

namespace NGOT.Infrastructure.EntityConfiguration;

public class CheckListItemEntityTypeConfiguration : IEntityTypeConfiguration<CheckListItem>
{
    public void Configure(EntityTypeBuilder<CheckListItem> builder)
    {
        builder.HasKey(ci => ci.Id);
    }
}