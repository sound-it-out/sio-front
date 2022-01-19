using Microsoft.AspNetCore.Components.Forms;
using SIO.Domain.TranslationOptions.Api.Responses;

namespace SIO.Domain.Documents.Api.Requests
{
    public record UploadRequest(byte[] File, string FileName, string FileContentType, string TranslationSubject, TranslationType TranslationType);
}
