using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SIO.Domain.TranslationOptions.Api.Responses;

namespace SIO.Domain.TranslationOptions.Api
{
    public interface ITranslationOptionApi
    {
        Task<ApiResponse<IEnumerable<TranslationOptionResponse>>> GetTranslationOptionsAsync(CancellationToken cancellationToken = default);
    }
}
