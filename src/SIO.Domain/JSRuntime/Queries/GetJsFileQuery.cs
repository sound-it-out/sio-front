using SIO.Infrastructure;
using SIO.Infrastructure.Queries;

namespace SIO.Domain.JSRuntime.Queries
{
    internal class GetJsFileQuery : Query<GetJsFileQueryResult>
    {
        public string File { get; set; }
        public GetJsFileQuery(string file) : base(null, Actor.Unknown)
        {
            File = file;
        }
    }
}
