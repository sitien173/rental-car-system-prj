using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NGOT.Common.Interfaces;

public interface IInstaller
{
    void InstallServices(IServiceCollection services, IConfiguration configuration);
}