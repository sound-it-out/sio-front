using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SIO.Domain.Documents.Api.Responses;
using SIO.Domain.Documents.States;
using SIO.Infrastructure.Events;
using SIO.IntegrationEvents.Documents;

namespace SIO.Domain.Documents.EventHandlers
{
    internal sealed class DocumentUploadedEventHandler : IEventHandler<DocumentUploaded>
    {
        private readonly ILogger<DocumentUploadedEventHandler> _logger;
        private readonly IUserDocumentsState _userDocumentsState;

        public DocumentUploadedEventHandler(ILogger<DocumentUploadedEventHandler> logger,
            IUserDocumentsState userDocumentsState)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));
            if (userDocumentsState == null)
                throw new ArgumentNullException(nameof(userDocumentsState));

            _logger = logger;
            _userDocumentsState = userDocumentsState;
        }

        public Task HandleAsync(IEventContext<DocumentUploaded> context, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation($"{nameof(DocumentUploadedEventHandler)}.{nameof(HandleAsync)} was cancelled before execution");
                cancellationToken.ThrowIfCancellationRequested();
            }

            var userDocuments = _userDocumentsState.Value.ToList();
            userDocuments.Add(new UserDocumentResponse(context.Payload.Id, context.Payload.FileName));

            _userDocumentsState.Load(userDocuments);

            _logger.LogInformation($"{nameof(DocumentUploadedEventHandler)}.{nameof(HandleAsync)} loaded new document with Id: {context.Payload.Id}, filename: {context.Payload.FileName}");

            return Task.CompletedTask;
        }
    }
}
