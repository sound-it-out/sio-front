using System.Collections.Generic;
using SIO.Domain.TranslationOptions.Api.Responses;
using SIO.Infrastructure.Queries;

namespace SIO.Domain.TranslationOptions.Queries
{
    public sealed class GetTranslationOptionsQueryResult : IQueryResult
    {
        public ApiResponse<IEnumerable<TranslationOptionResponse>> Response {  get; }

        public GetTranslationOptionsQueryResult(ApiResponse<IEnumerable<TranslationOptionResponse>> response)
        {
            Response = response;
        }
    }
}
