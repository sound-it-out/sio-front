using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SIO.Domain.Documents.Api.Requests;
using SIO.Domain.Documents.Api.Responses;

namespace SIO.Domain.Documents.Api
{
    public interface IDocumentApi
    {
        Task<ApiResponse> UploadAsync(UploadRequest request, CancellationToken cancellationToken = default);
        Task<ApiResponse<FileResponse>> DownloadAsync(string subject, CancellationToken cancellationToken = default);
        Task<ApiResponse<IEnumerable<UserDocumentResponse>>> GetAsync(CancellationToken cancellationToken = default);
    }
}
