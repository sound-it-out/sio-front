using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SIO.Domain.Documents.Api;
using SIO.Domain.Documents.Commands;
using SIO.Domain.Documents.States;
using SIO.Infrastructure.Commands;

namespace SIO.Domain.Documents.CommandHandlers
{
    internal sealed class LoadUserDocumentsStateCommandHandler : ICommandHandler<LoadUserDocumentsStateCommand>
    {        
        private readonly ILogger<LoadUserDocumentsStateCommandHandler> _logger;
        private readonly IDocumentApi _documentApi;
        private readonly IUserDocumentsState _userDocumentsState;

        public LoadUserDocumentsStateCommandHandler(ILogger<LoadUserDocumentsStateCommandHandler> logger,
            IDocumentApi documentApi,
            IUserDocumentsState userDocumentsState)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));
            if (documentApi == null)
                throw new ArgumentNullException(nameof(documentApi));
            if (userDocumentsState == null)
                throw new ArgumentNullException(nameof(userDocumentsState));

            _logger = logger;
            _documentApi = documentApi;
            _userDocumentsState = userDocumentsState;
        }

        public async Task ExecuteAsync(LoadUserDocumentsStateCommand command, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation($"{nameof(LoadUserDocumentsStateCommandHandler)}.{nameof(ExecuteAsync)} was cancelled before execution");
                cancellationToken.ThrowIfCancellationRequested();
            }

            var documentsResponse = await _documentApi.GetAsync(cancellationToken);

            if (documentsResponse.IsError)
            {
                _logger.LogCritical($"{nameof(LoadUserDocumentsStateCommandHandler)}.{nameof(ExecuteAsync)} failure to retrieve user documents, error: {documentsResponse.Error}");
                return;
            }

            _userDocumentsState.Load(documentsResponse.Result);
        }
    }
}
