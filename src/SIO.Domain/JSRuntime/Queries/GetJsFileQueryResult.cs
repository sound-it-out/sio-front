using Microsoft.JSInterop;
using SIO.Infrastructure.Queries;

namespace SIO.Domain.JSRuntime.Queries
{
    public class GetJsFileQueryResult : IQueryResult
    {
        public GetJsFileQueryResult(IJSObjectReference jSObjectReference)
        {
            JSObjectReference = jSObjectReference;
        }

        public IJSObjectReference JSObjectReference {get;set;}
    }
}
