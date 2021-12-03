using System.Threading;
using System.Threading.Tasks;
using SIO.Domain.Documents.Api.Requests;

namespace SIO.Domain.Documents.Api
{
    public interface IDocumentApi
    {
        Task<ApiResponse> UploadAsync(UploadRequest request, CancellationToken cancellationToken);
    }
}
