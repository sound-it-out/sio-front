using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            if(builder.HostEnvironment.IsDevelopment())
                builder.Logging.SetMinimumLevel(LogLevel.Debug);
            else
                builder.Logging.SetMinimumLevel(LogLevel.Critical);

            builder.Services.AddSIOInfrastructure()
                .AddCommands()
                .DisableCommandStore()
                .AddQueries()
                .DisableQueryStore();

            builder.Services
                .AddComponents()
                .AddDomain(builder.HostEnvironment)                
                .AddAuthentication(builder.Configuration)
                .AddLogging()
                .AddApiAuthorization();

            await builder.Build().RunAsync();
        }
    }
}
