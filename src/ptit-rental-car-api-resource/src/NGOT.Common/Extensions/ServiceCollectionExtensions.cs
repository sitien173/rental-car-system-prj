using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NGOT.Common.Interfaces;

namespace NGOT.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ScanServiceFromAssemblyContaining<T>(this IServiceCollection services,
        IConfiguration configuration)
    {
        var assembly = typeof(T).Assembly;
        var type = typeof(IInstaller);

        var installers = assembly.ExportedTypes
            .Where(x => type.IsAssignableFrom(x) && x is { IsInterface: false, IsAbstract: false })
            .Select(Activator.CreateInstance)
            .Cast<IInstaller>()
            .ToList();

        services.Scan(x =>
            x.FromAssemblyOf<T>()
                .AddClasses(classes => classes.AssignableTo<ITransient>())
                .AsImplementedInterfaces()
                .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo<ISingleton>())
                .AsImplementedInterfaces()
                .WithSingletonLifetime()
                .AddClasses(classes => classes.AssignableTo<IScoped>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );

        installers.ForEach(installer => installer.InstallServices(services, configuration));
        return services;
    }
}