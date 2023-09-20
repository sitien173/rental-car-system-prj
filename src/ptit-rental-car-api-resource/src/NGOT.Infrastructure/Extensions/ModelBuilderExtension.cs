using Microsoft.EntityFrameworkCore;

namespace NGOT.Infrastructure.Extensions;

public static class ModelBuilderExtension
{
    public static void ApplyEnumToStringConversion(this ModelBuilder modelBuilder)
    {
        var entityTypes = modelBuilder.Model.GetEntityTypes();

        foreach (var entityType in entityTypes)
        {
            var properties = entityType.ClrType.GetProperties()
                .Where(p => p.PropertyType.IsEnum);

            foreach (var property in properties)
            {
                var builder = modelBuilder.Entity(entityType.ClrType)
                    .Property(property.Name);

                builder.HasConversion<string>();
            }
        }
    }
}