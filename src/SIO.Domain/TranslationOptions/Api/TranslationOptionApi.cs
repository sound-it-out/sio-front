using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SIO.Domain.TranslationOptions.Api.Responses;

namespace SIO.Domain.TranslationOptions.Api
{
    internal sealed class TranslationOptionApi : ApiBase, ITranslationOptionApi
    {
        public TranslationOptionApi(HttpClient httpClient, IOptions<ApiOptions> options) : base(httpClient, options, "translationoption")
        {
        }

        public Task<ApiResponse<IEnumerable<TranslationOptionResponse>>> GetTranslationOptionsAsync(CancellationToken cancellationToken = default) 
            => GetAsync<IEnumerable<TranslationOptionResponse>>(cancellationToken: cancellationToken);
    }
}
