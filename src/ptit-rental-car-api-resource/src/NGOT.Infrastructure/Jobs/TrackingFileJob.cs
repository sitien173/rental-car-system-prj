using System.Data;
using AsyncAwaitBestPractices;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGOT.Common.Extensions;
using NGOT.Common.Interfaces;
using Serilog;

namespace NGOT.Infrastructure.Jobs;

public class TrackingFileJob : BackgroundService
{
    private const string _jobName = nameof(TrackingFileJob);
    private readonly ILogger _logger = Log.ForContext<TrackingFileJob>();
    private readonly IServiceProvider _serviceProvider;

    public TrackingFileJob(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DoWork(CancellationToken cancellationToken)
    {
        using var scoped = _serviceProvider.CreateScope();
        var filesInDb = await GetFilesInDbAsync(scoped, cancellationToken);
        var filesInFolder = await GetFilesInFolder(scoped);

        var fileProvider = scoped.ServiceProvider.GetRequiredService<IFileProvider>();
        filesInFolder.Except(filesInDb).ForEach(x =>
        {
            fileProvider.DeleteFileAsync(x, cancellationToken).SafeFireAndForget();
        });
    }

    public static async Task<List<string>> GetFilesInFolder(IServiceScope serviceScope)
    {
        var fileService = serviceScope.ServiceProvider.GetRequiredService<IFileProvider>();
        var files = await fileService.GetAllFilesAsync();
        return files.ToList();
    }

    public static async Task<List<string>> GetFilesInDbAsync(IServiceScope serviceScope, CancellationToken ct)
    {
        var filesInDb = new List<string>();
        await using var dbContext = serviceScope.ServiceProvider.GetRequiredService<DbContext>();
        {
            await using var command = dbContext.Database.GetDbConnection().CreateCommand();
            {
                command.CommandText = "[dbo].[GetFilesInDb]";
                command.CommandType = CommandType.StoredProcedure;
                await dbContext.Database.OpenConnectionAsync(ct);
                await using var reader = await command.ExecuteReaderAsync(ct);
                {
                    while (await reader.ReadAsync(ct)) filesInDb.Add(reader.GetString(0));
                }
            }
        }
        return filesInDb;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.Information("{JobName} is running", _jobName);
        // Enqueue a daily recurring job
        RecurringJob.AddOrUpdate(_jobName, () => DoWork(stoppingToken), Cron.Monthly);
        return Task.CompletedTask;
    }
}