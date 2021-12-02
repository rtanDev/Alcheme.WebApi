using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alcheme.WebApi.Installers
{
    public interface IInstallerService
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}