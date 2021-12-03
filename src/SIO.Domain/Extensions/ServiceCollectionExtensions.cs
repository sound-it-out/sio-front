using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SIO.Domain.Documents.Api;
using SIO.Domain.Documents.CommandHandlers;
using SIO.Domain.Documents.Commands;
using SIO.Domain.TranslationOptions.Api;
using SIO.Domain.TranslationOptions.Queries;
using SIO.Domain.TranslationOptions.QueryHandlers;
using SIO.Infrastructure.Commands;
using SIO.Infrastructure.Queries;

namespace SIO.Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services, IWebAssemblyHostEnvironment environment)
        {
            services.ConfigureApis(environment)
                .AddDocuments()
                .AddTranslationOptions();

            return services;
        }

        private static IServiceCollection AddTranslationOptions(this IServiceCollection services)
        {
            //Queries
            services.AddScoped<IQueryHandler<GetTranslationOptionsQuery, GetTranslationOptionsQueryResult>, GetTranslationOptionsQueryHandler>();
            //Apis
            services.AddApi<ITranslationOptionApi, TranslationOptionApi>();

            return services;
        }

        private static IServiceCollection AddDocuments(this IServiceCollection services)
        {
            //Queries
            services.AddScoped<ICommandHandler<UploadDocumentCommand>, UploadDocumentCommandHandler>();
            //Apis
            services.AddApi<IDocumentApi, DocumentApi>();

            return services;
        }

        private static IServiceCollection ConfigureApis(this IServiceCollection services, IWebAssemblyHostEnvironment environment)
        {
            if (environment.IsDevelopment())
                services.Configure<ApiOptions>(o => o.BaseUrl = "https://localhost:44363/v1");

            return services;
        }

        public static IServiceCollection AddApi<TClient, TImplementation>(this IServiceCollection services)
            where TClient : class 
            where TImplementation : class, TClient
        {
            services.AddHttpClient<TClient, TImplementation>()
                .AddHttpMessageHandler(sp =>
                {
                    var apiOptions = sp.GetRequiredService<IOptions<ApiOptions>>();
                    var handler = sp.GetRequiredService<AuthorizationMessageHandler>()
                        .ConfigureHandler(authorizedUrls: new[] { apiOptions.Value.BaseUrl });

                    return handler;
                });

            return services;
        }
    }
}
