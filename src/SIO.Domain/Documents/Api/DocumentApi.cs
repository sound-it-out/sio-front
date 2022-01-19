using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SIO.Domain.Documents.Api.Requests;
using SIO.Domain.Documents.Api.Responses;

namespace SIO.Domain.Documents.Api
{
    internal sealed class DocumentApi : ApiBase, IDocumentApi
    {
        public DocumentApi(HttpClient httpClient, IOptions<ApiOptions> options) : base(httpClient, options, "document")
        {
        }

        public Task<ApiResponse<FileResponse>> DownloadAsync(string subject, CancellationToken cancellationToken = default)
            => DownloadFileAsync($"download/{subject}", cancellationToken: cancellationToken);

        public Task<ApiResponse<IEnumerable<UserDocumentResponse>>> GetAsync(CancellationToken cancellationToken = default)
            => GetAsync<IEnumerable<UserDocumentResponse>>(cancellationToken: cancellationToken);

        public async Task<ApiResponse> UploadAsync(UploadRequest request, CancellationToken cancellationToken = default)
        {
            var content = new MultipartFormDataContent();
            var fileContent = new ByteArrayContent(request.File);

            fileContent.Headers.ContentType = new MediaTypeHeaderValue(request.FileContentType);

            content.Add(fileContent, nameof(UploadRequest.File), request.FileName);
            content.Add(new StringContent(request.TranslationType.ToString()), nameof(UploadRequest.TranslationType));
            content.Add(new StringContent(request.TranslationSubject), nameof(UploadRequest.TranslationSubject));

            var message = new HttpRequestMessage(HttpMethod.Post, "upload")
            {
                Content = content
            };

            message.Headers.Add("accept", "application/json");

            return await ProcessResponse(await _httpClient.SendAsync(message));
        }
    }
}
