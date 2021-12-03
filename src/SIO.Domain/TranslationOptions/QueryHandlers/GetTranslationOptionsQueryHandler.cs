using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SIO.Domain.TranslationOptions.Api;
using SIO.Domain.TranslationOptions.Queries;
using SIO.Infrastructure.Queries;

namespace SIO.Domain.TranslationOptions.QueryHandlers
{
    internal sealed class GetTranslationOptionsQueryHandler : IQueryHandler<GetTranslationOptionsQuery, GetTranslationOptionsQueryResult>
    {
        private readonly ILogger<GetTranslationOptionsQueryHandler> _logger;
        private readonly ITranslationOptionApi _translationOptionsApi;

        public GetTranslationOptionsQueryHandler(ILogger<GetTranslationOptionsQueryHandler> logger,
            ITranslationOptionApi translationOptionsApi)
        {
            if(logger == null)
                throw new ArgumentNullException(nameof(logger));
            if (translationOptionsApi == null)
                throw new ArgumentNullException(nameof(translationOptionsApi));

            _logger = logger;
            _translationOptionsApi = translationOptionsApi;
        }

        public async Task<GetTranslationOptionsQueryResult> RetrieveAsync(GetTranslationOptionsQuery query, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation($"{nameof(GetTranslationOptionsQueryHandler)}.{nameof(RetrieveAsync)} was cancelled before execution");
                cancellationToken.ThrowIfCancellationRequested();
            }

            var translationOptionsResponse = await _translationOptionsApi.GetTranslationOptionsAsync(cancellationToken);
            return new GetTranslationOptionsQueryResult(translationOptionsResponse);
        }
    }
}
