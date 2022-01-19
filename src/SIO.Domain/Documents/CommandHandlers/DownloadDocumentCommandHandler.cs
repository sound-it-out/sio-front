using System;
using System.Threading;
using System.Threading.Tasks;
using SIO.Domain.Documents.Api;
using SIO.Domain.Documents.Commands;
using SIO.Domain.JSRuntime.Commands;
using SIO.Infrastructure.Commands;

namespace SIO.Domain.Documents.Commandhandlers
{
    internal sealed class DownloadDocumentCommandHandler : ICommandHandler<DownloadDocumentCommand>
    {
        private readonly IDocumentApi _documentApi;
        private readonly ICommandDispatcher _commandDispatcher;

        public DownloadDocumentCommandHandler(IDocumentApi documentApi,
            ICommandDispatcher commandDispatcher)
        {
            if (documentApi is null)
                throw new ArgumentNullException(nameof(documentApi));

            if (commandDispatcher is null)
                throw new ArgumentNullException(nameof(commandDispatcher));

            _documentApi = documentApi;
            _commandDispatcher = commandDispatcher;
        }

        public async Task ExecuteAsync(DownloadDocumentCommand command, CancellationToken cancellationToken = default)
        {
            var fileResponse = await _documentApi.DownloadAsync(command.Subject, cancellationToken);

            if(!fileResponse.IsError)
                await _commandDispatcher.DispatchAsync(new DownloadFileCommand(fileResponse.Result.FileName, fileResponse.Result.Stream));
        }
    }
}
