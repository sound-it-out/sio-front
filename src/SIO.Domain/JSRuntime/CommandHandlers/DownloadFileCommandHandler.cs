using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using SIO.Domain.JSRuntime.Commands;
using SIO.Domain.JSRuntime.Queries;
using SIO.Infrastructure.Commands;
using SIO.Infrastructure.Queries;

namespace SIO.Domain.JSRuntime.CommandHandlers
{
    internal class DownloadFileCommandHandler : ICommandHandler<DownloadFileCommand>
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public DownloadFileCommandHandler(IQueryDispatcher queryDispatcher)
        {
            if (queryDispatcher is null)
                throw new ArgumentNullException(nameof(queryDispatcher));

            _queryDispatcher = queryDispatcher;
        }

        public async Task ExecuteAsync(DownloadFileCommand command, CancellationToken cancellationToken = default)
        {
            var jsFileResult = await _queryDispatcher.DispatchAsync(new GetJsFileQuery("./js/download-file.js"), cancellationToken);
            using var streamRef = new DotNetStreamReference(stream: command.Stream);
            await jsFileResult.JSObjectReference.InvokeVoidAsync("downloadFileFromStream", command.Subject, streamRef);
        }
    }
}
