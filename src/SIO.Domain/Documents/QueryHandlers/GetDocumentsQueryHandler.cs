using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SIO.Domain.Documents.Api;
using SIO.Domain.Documents.Api.Responses;
using SIO.Domain.Documents.Queries;
using SIO.Domain.Documents.States;
using SIO.Infrastructure.Queries;

namespace SIO.Domain.Documents.QueryHandlers
{
    internal sealed class GetDocumentsQueryHandler : IQueryHandler<GetDocumentsQuery, GetDocumentsQueryResult>
    {
        private readonly ILogger<GetDocumentsQueryHandler> _logger;
        private readonly IUserDocumentsState _userDocumentsState;

        public GetDocumentsQueryHandler(ILogger<GetDocumentsQueryHandler> logger,
            IUserDocumentsState userDocumentsState)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));
            if (userDocumentsState == null)
                throw new ArgumentNullException(nameof(userDocumentsState));

            _logger = logger;
            _userDocumentsState = userDocumentsState;
        }

        public Task<GetDocumentsQueryResult> RetrieveAsync(GetDocumentsQuery query, CancellationToken cancellationToken = default)
        {
            try
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogInformation($"{nameof(GetDocumentsQueryHandler)}.{nameof(RetrieveAsync)} was cancelled before execution");
                    cancellationToken.ThrowIfCancellationRequested();
                }

                return Task.FromResult(new GetDocumentsQueryResult(_userDocumentsState?.Value ?? Enumerable.Empty<UserDocumentResponse>()));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                throw;
            }
        }
    }
}
