using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SIO.Front.Client.Extensions;
using System.Threading.Tasks;

namespace SIO.Front.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services
                .AddApi(builder.Configuration)
                .AddAuthentication(builder.Configuration)
                .AddApiAuthorization();

            await builder.Build().RunAsync();
        }
    }
}
