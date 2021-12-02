using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SIO.Api.Client.Api;
using SIO.Front.Client.Components;
using System.Net.Http;

namespace SIO.Front.Client.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        private const string ApiClient = nameof(ApiClient);

        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDocumentApiAsync>(sp => new DocumentApi(sp.GetService<IHttpClientFactory>().CreateClient(ApiClient), "https://localhost:44363", null));
            services.AddScoped<IUserDocumentsProjectionApiAsync>(sp => new UserDocumentsProjectionApi(sp.GetService<IHttpClientFactory>().CreateClient(ApiClient), "https://localhost:44363", null));
            services.AddScoped<ITranslationApiAsync>(sp => new TranslationApi(sp.GetService<IHttpClientFactory>().CreateClient(ApiClient), "https://localhost:44363", null));

            services.AddHttpClient(ApiClient)
                .AddHttpMessageHandler(sp =>
                {
                    var handler = sp.GetService<AuthorizationMessageHandler>()
                        .ConfigureHandler(authorizedUrls: new[] { "https://localhost:44363" });

                    return handler;
                });

            return services;
        }

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
