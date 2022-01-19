using System.Collections.Generic;
using SIO.Domain.Documents.Api.Responses;
using SIO.Infrastructure.Queries;

namespace SIO.Domain.Documents.Queries
{
    public sealed class GetDocumentsQueryResult : IQueryResult
    {
        public IEnumerable<UserDocumentResponse> UserDocuments {  get; }

        public GetDocumentsQueryResult(IEnumerable<UserDocumentResponse> userDocuments)
        {
            UserDocuments = userDocuments;
        }
    }
}
