using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SIO.Domain.Documents.Api;
using SIO.Domain.Documents.Commandhandlers;
using SIO.Domain.Documents.CommandHandlers;
using SIO.Domain.Documents.Commands;
using SIO.Domain.Documents.EventHandlers;
using SIO.Domain.Documents.Queries;
using SIO.Domain.Documents.QueryHandlers;
using SIO.Domain.Documents.States;
using SIO.Domain.JSRuntime.CommandHandlers;
using SIO.Domain.JSRuntime.Commands;
using SIO.Domain.JSRuntime.Queries;
using SIO.Domain.JSRuntime.QueryHandlers;
using SIO.Domain.TranslationOptions.Api;
using SIO.Domain.TranslationOptions.Queries;
using SIO.Domain.TranslationOptions.QueryHandlers;
using SIO.Infrastructure.Commands;
using SIO.Infrastructure.Events;
using SIO.Infrastructure.Queries;
using SIO.IntegrationEvents.Documents;

namespace SIO.Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services, IWebAssemblyHostEnvironment environment)
        {
            services.ConfigureApis(environment)
                .AddDocuments()
                .AddTranslationOptions()
                .AddJsRUntime();

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
            services.AddScoped<IQueryHandler<GetDocumentsQuery, GetDocumentsQueryResult>, GetDocumentsQueryHandler>();
            //Commands
            services.AddScoped<ICommandHandler<UploadDocumentCommand>, UploadDocumentCommandHandler>();
            services.AddScoped<ICommandHandler<LoadUserDocumentsStateCommand>, LoadUserDocumentsStateCommandHandler>();
            services.AddScoped<ICommandHandler<DownloadDocumentCommand>, DownloadDocumentCommandHandler>();
            //Event handlers
            services.AddScoped<IEventHandler<DocumentUploaded>, DocumentUploadedEventHandler>();
            //Apis
            services.AddApi<IDocumentApi, DocumentApi>();
            //States
            services.AddSingleton<IUserDocumentsState, UserDocumentsState>();

            return services;
        }

        private static IServiceCollection AddJsRUntime(this IServiceCollection services)
        {
            //Queries
            services.AddScoped<IQueryHandler<GetJsFileQuery, GetJsFileQueryResult>, GetJsFileQueryHandler>();

            //Commands
            services.AddScoped<ICommandHandler<DownloadFileCommand>, DownloadFileCommandHandler>();            

            return services;
        }

        private static IServiceCollection ConfigureApis(this IServiceCollection services, IWebAssemblyHostEnvironment environment)
        {
            if (environment.IsDevelopment())
                services.Configure<ApiOptions>(o => o.BaseUrl = "https://localhost:44380/v1");

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
