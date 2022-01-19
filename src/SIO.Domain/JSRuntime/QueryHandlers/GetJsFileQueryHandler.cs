using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using SIO.Domain.JSRuntime.Queries;
using SIO.Infrastructure.Queries;

namespace SIO.Domain.JSRuntime.QueryHandlers
{
    internal sealed class GetJsFileQueryHandler : IQueryHandler<GetJsFileQuery, GetJsFileQueryResult>
    {
        private readonly IJSRuntime _jSRuntime;
        private readonly Dictionary<string, IJSObjectReference> _loadedFiles;

        public GetJsFileQueryHandler(IJSRuntime jSRuntime)
        {
            if (jSRuntime is null)
                throw new ArgumentNullException(nameof(jSRuntime));

            _jSRuntime = jSRuntime;
            _loadedFiles = new();
        }

        public async Task<GetJsFileQueryResult> RetrieveAsync(GetJsFileQuery query, CancellationToken cancellationToken = default)
        {
            if(!_loadedFiles.TryGetValue(query.File, out var jSObjectReference))
            {
                jSObjectReference = await _jSRuntime.InvokeAsync<IJSObjectReference>("import", query.File);
                _loadedFiles.Add(query.File, jSObjectReference);
            }

            return new GetJsFileQueryResult(jSObjectReference);
        }
    }
}
