using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SIO.Domain.Documents.Api.Requests;

namespace SIO.Domain.Documents.Api
{
    internal sealed class DocumentApi : ApiBase, IDocumentApi
    {
        public DocumentApi(HttpClient httpClient, IOptions<ApiOptions> options) : base(httpClient, options, "document")
        {
        }

        public async Task<ApiResponse> UploadAsync(UploadRequest request, CancellationToken cancellationToken = default)
        {
            var content = new MultipartFormDataContent();
            Console.WriteLine("Adding file");
            var fileContent = new ByteArrayContent(request.File);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(request.FileContentType);
            content.Add(fileContent, nameof(UploadRequest.File), request.FileName);
            Console.WriteLine("File added");
            content.Add(new StringContent(request.TranslationType.ToString()), nameof(UploadRequest.TranslationType));
            content.Add(new StringContent(request.TranslationSubject), nameof(UploadRequest.TranslationSubject));

            var message = new HttpRequestMessage(HttpMethod.Post, "upload")
            {
                Content = content
            };
            message.Headers.Add("accept", "application/json");

            Console.WriteLine("Requesting");

            return await ProcessResponse(await _httpClient.SendAsync(message));
        }
    }
}
