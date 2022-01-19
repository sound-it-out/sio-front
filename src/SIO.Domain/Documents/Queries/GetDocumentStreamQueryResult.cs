using System.IO;
using SIO.Infrastructure.Queries;

namespace SIO.Domain.Documents.Queries
{
    public class GetDocumentStreamQueryResult : IQueryResult
    {
        public GetDocumentStreamQueryResult(Stream stream)
        {
            Stream = stream;
        }

        public Stream Stream {  get; set; }
    }
}
