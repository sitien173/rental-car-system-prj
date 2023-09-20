using System.Text;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NGOT.API.Services;
using NGOT.Common.Interfaces;
using NGOT.Common.Settings;
using Serilog;

namespace NGOT.API;

public class AppModule : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
        services.Scan(x =>
            x.FromAssemblyOf<AppModule>()
                .AddClasses(classes => classes.AssignableTo(typeof(IConfigureOptions<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        if (isDevelopment)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("x-api-version"),
                    new MediaTypeApiVersionReader("x-api-version"));
            });
            // Add ApiExplorer to discover versions
            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            services.AddTransient<IFileProvider, FileProviderDevelopment>();
        }

        services.AddSerilog(opt => { opt.ReadFrom.Configuration(configuration); });

        services.AddControllers()
            .AddNewtonsoftJson();

        services.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();

        services.AddHttpContextAccessor();
        services.AddHttpClient();

        services.AddAutoMapper(typeof(AppModule));

        services.AddAuthorization();

        var jwtSettings = new JwtSettings();
        var externalLoginSettings = new ExternalLoginSettings();
        configuration.Bind(jwtSettings.SectionName, jwtSettings);
        configuration.Bind(externalLoginSettings.SectionName, externalLoginSettings);
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                // use identity server and jwt token
                options.Authority = jwtSettings.Authority;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Authority,

                    ValidateAudience = false,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };
            });
    }
}