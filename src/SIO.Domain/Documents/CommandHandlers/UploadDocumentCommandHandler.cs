using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SIO.Domain.Documents.Api;
using SIO.Domain.Documents.Api.Requests;
using SIO.Domain.Documents.Commands;
using SIO.Infrastructure.Commands;

namespace SIO.Domain.Documents.CommandHandlers
{
    internal sealed class UploadDocumentCommandHandler : ICommandHandler<UploadDocumentCommand>
    {
        private readonly ILogger<UploadDocumentCommandHandler> _logger;
        private readonly IDocumentApi _documentApi;

        public UploadDocumentCommandHandler(ILogger<UploadDocumentCommandHandler> logger,
            IDocumentApi documentApi)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));
            if (documentApi == null)
                throw new ArgumentNullException(nameof(documentApi));

            _logger = logger;
            _documentApi = documentApi;
        }

        public async Task ExecuteAsync(UploadDocumentCommand command, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation($"{nameof(UploadDocumentCommandHandler)}.{nameof(ExecuteAsync)} was cancelled before execution");
                cancellationToken.ThrowIfCancellationRequested();
            }

            await _documentApi.UploadAsync(new UploadRequest(command.File, command.FileName, command.FileContentType, command.TranslationOption.Subject, command.TranslationOption.TranslationType), cancellationToken);
        }
    }
}
