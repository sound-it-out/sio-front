using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SIO.Domain.Extensions;
using SIO.Front.Client.Extensions;
using SIO.Infrastructure.Extensions;
using SIO.Infrastructure.Serialization.MessagePack.Extensions;
using System.Threading.Tasks;

namespace SIO.Front.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddSIOInfrastructure()
                .AddCommands()
                .DisableCommandLogging()
                .AddQueries()
                .DisableQueryLogging();

            builder.Services
                .AddComponents()
                .AddDomain(builder.HostEnvironment)                
                .AddAuthentication(builder.Configuration)
                .AddApiAuthorization();

            await builder.Build().RunAsync();
        }
    }
}
