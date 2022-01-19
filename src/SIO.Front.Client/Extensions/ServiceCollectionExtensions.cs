using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SIO.Front.Client.Components;

namespace SIO.Front.Client.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOidcAuthentication(options =>
            {
                configuration.Bind("Identity", options.ProviderOptions);
                options.ProviderOptions.Authority = "https://sio.identity:5001";
                options.UserOptions.RoleClaim = "role";
            });

            return services;
        }

        public static IServiceCollection AddComponents(this IServiceCollection services)
        {
            services.AddScoped<IWizardInteraction, WizardInteraction>();
            return services;
        }
    }
}
