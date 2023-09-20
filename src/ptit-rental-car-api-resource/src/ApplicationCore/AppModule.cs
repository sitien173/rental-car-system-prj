using Ardalis.GuardClauses;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NGOT.Common.Interfaces;
using NGOT.Common.Settings;

namespace NGOT.ApplicationCore;

public class AppModule : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(AppModule));

        services.AddValidatorsFromAssemblyContaining<AppModule>();

        ScanAndRegistrationSetting(services, configuration);
    }

    private static void ScanAndRegistrationSetting(IServiceCollection services, IConfiguration configuration)
    {
        var settings = typeof(ISettings).Assembly.ExportedTypes
            .Where(x => x is { IsInterface: false, IsAbstract: false } && typeof(ISettings).IsAssignableFrom(x))
            .Select(Activator.CreateInstance)
            .Cast<ISettings>()
            .ToList();

        settings.ForEach(x =>
        {
            var sectionName = x.SectionName;
            var section = configuration.GetSection(sectionName);
            if (!section.Exists())
                return;

            var instance = Activator.CreateInstance(x.GetType());
            Guard.Against.Null(instance);
            section.Bind(instance);

            services.AddSingleton(x.GetType(), instance);

            var optionWrapperType = typeof(OptionsWrapper<>).MakeGenericType(x.GetType());
            var optionWrapperInstance = Activator.CreateInstance(optionWrapperType, instance);
            Guard.Against.Null(optionWrapperInstance);

            services.AddSingleton(typeof(IOptions<>).MakeGenericType(x.GetType()), optionWrapperInstance);
        });
    }
}