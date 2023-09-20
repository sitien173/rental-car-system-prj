using System.Threading.RateLimiting;
using Hangfire;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using NGOT.API;
using NGOT.API.ErrorHandlers;
using NGOT.Common.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("serilog.json");

builder.Services.ScanServiceFromAssemblyContaining<NGOT.ApplicationCore.AppModule>(builder.Configuration)
    .ScanServiceFromAssemblyContaining<NGOT.Infrastructure.AppModule>(builder.Configuration)
    .ScanServiceFromAssemblyContaining<AppModule>(builder.Configuration);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in app.Services.GetRequiredService<IApiVersionDescriptionProvider>()
                     .ApiVersionDescriptions)
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
    });

    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DbContext>();
    await context.Database.MigrateAsync();

    app.UseStaticFiles();
}

app.UseSerilogRequestLogging();
app.UseCors(x =>
{
    x.AllowAnyOrigin();
    x.AllowAnyMethod();
    x.AllowAnyHeader();
});
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter(new RateLimiterOptions
{
    GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(_ =>
    {
        return RateLimitPartition.GetConcurrencyLimiter<string>("General", s => new ConcurrencyLimiterOptions
        {
            PermitLimit = 1,
            QueueLimit = 100,
            QueueProcessingOrder = QueueProcessingOrder.NewestFirst
        });
    })
});
app.UseHangfireDashboard();
app.UseMiddleware<GlobalHandleErrorMiddleware>();
app.MapDefaultControllerRoute();
app.Run();