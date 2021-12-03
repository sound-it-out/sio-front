using Microsoft.AspNetCore.Components.Forms;
using SIO.Domain.TranslationOptions.Api.Responses;
using SIO.Infrastructure;
using SIO.Infrastructure.Commands;

namespace SIO.Domain.Documents.Commands
{
    public class UploadDocumentCommand : Command
    {
        public byte[] File { get; }
        public string FileName { get; }
        public string FileContentType { get; }
        public TranslationOptionResponse TranslationOption { get; }

        public UploadDocumentCommand(byte[] file, string fileName, string fileContentType, TranslationOptionResponse translationOption) : base(Infrastructure.Subject.New(), null, 0, Actor.Unknown)
        {
            File = file;
            FileContentType = fileContentType;
            FileName = fileName;
            TranslationOption = translationOption;
        }
    }
}
