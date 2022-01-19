using SIO.Infrastructure;
using SIO.Infrastructure.Queries;

namespace SIO.Domain.Documents.Queries
{
    public sealed class GetDocumentsQuery : Query<GetDocumentsQueryResult>
    {
        public GetDocumentsQuery() : base(null, Actor.Unknown)
        {
        }
    }
}
