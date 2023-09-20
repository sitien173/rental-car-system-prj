using FluentEmail.MailKitSmtp;
using Google.Cloud.Storage.V1;
using Hangfire;
using HibernatingRhinos.Profiler.Appender.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NGOT.Common.Interfaces;
using NGOT.Common.Settings;
using NGOT.Infrastructure.Data;
using NGOT.Infrastructure.Interceptors;
using NGOT.Infrastructure.Jobs;

namespace NGOT.Infrastructure;

public class AppModule : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        var appConnectionString = configuration.GetConnectionString("ApplicationDbConnection")!;
        var hangfireConnectionString = configuration.GetConnectionString("HangfireJobConnection")!;

        services.AddHostedService<TrackingFileJob>();

        services.AddSingleton<StorageClient>(x =>
        {
            var storeSettings = x.GetRequiredService<GoogleCloudStorageSettings>();

            StorageClientBuilder storageClientBuilder = new()
            {
                JsonCredentials = JsonConvert.SerializeObject(storeSettings.Credential)
            };
            return storageClientBuilder.Build();
        });

        EntityFrameworkProfiler.Initialize();

        services.AddScoped<ISaveChangesInterceptor, AuditableEntitySaveChangesInterceptor>();

        services.AddDbContextPool<DbContext, ApplicationDbContext>(opt =>
        {
            opt.UseSqlServer(appConnectionString, builder =>
            {
                builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                builder.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
                builder.MaxBatchSize(1000);
                builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                builder.MigrationsHistoryTable("__MigrationsHistoryForApplicationDbContext");
            });
        });

        services.AddHangfire(opt =>
        {
            opt.UseSqlServerStorage(hangfireConnectionString);
            opt.UseSerilogLogProvider();
        });

        var emailSettings = configuration.GetSection(nameof(MailSettings)).Get<MailSettings>();
        services.AddFluentEmail(emailSettings.Username)
            .AddMailKitSender(new SmtpClientOptions
            {
                User = emailSettings.Username,
                Password = emailSettings.Password,
                Server = emailSettings.Host,
                Port = emailSettings.Port,
                UseSsl = emailSettings.UseSsl,
                RequiresAuthentication = true
            });
        services.AddHangfireServer();
    }
}