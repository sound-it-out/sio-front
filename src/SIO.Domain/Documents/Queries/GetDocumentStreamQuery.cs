using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIO.Infrastructure;
using SIO.Infrastructure.Queries;

namespace SIO.Domain.Documents.Queries
{
    public class GetDocumentStreamQuery : Query<GetDocumentStreamQueryResult>
    {
        public GetDocumentStreamQuery(Subject subject) : base(null, Actor.Unknown)
        {
        }

        public Subject Subject {  get; set; }

    }
}
