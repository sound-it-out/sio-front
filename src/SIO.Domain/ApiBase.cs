using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace SIO.Domain
{
    internal abstract class ApiBase
    {        
        private static readonly HttpStatusCode[] _validStatusCodes = new[] 
        { 
            HttpStatusCode.OK, 
            HttpStatusCode.Accepted, 
            HttpStatusCode.Created,
            HttpStatusCode.Processing,
            HttpStatusCode.NotModified,
            HttpStatusCode.NoContent
        };

        protected readonly HttpClient _httpClient;

        public ApiBase(HttpClient httpClient, IOptions<ApiOptions> options, string route)
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));
            if(options == null)
                throw new ArgumentNullException(nameof(options));

            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri($"{options.Value.BaseUrl}/{route}/");
        }

        protected async Task<ApiResponse<T>> GetAsync<T>(string requestUri = null, CancellationToken cancellationToken = default) 
            => await ProcessResponse<T>(await _httpClient.GetAsync(requestUri, cancellationToken));
        protected async Task<ApiResponse> DeleteAsync(string requestUri = null, CancellationToken cancellationToken = default) 
            => await ProcessResponse(await _httpClient.DeleteAsync(requestUri, cancellationToken));
        protected async Task<ApiResponse> PostAsync<T>(string requestUri = null, T data = default, CancellationToken cancellationToken = default)
            => await PostAsync(requestUri, content: new StringContent(JsonConvert.SerializeObject(data)), cancellationToken);
        protected async Task<ApiResponse> PostAsync(string requestUri = null, HttpContent content = default, CancellationToken cancellationToken = default)
            => await ProcessResponse(await _httpClient.PostAsync(requestUri, content, cancellationToken));
        protected async Task<ApiResponse> PutAsync<T>(string requestUri = null, T data = default, CancellationToken cancellationToken = default) 
            => await ProcessResponse(await _httpClient.PutAsync(requestUri, new StringContent(JsonConvert.SerializeObject(data)), cancellationToken));        

        protected async Task<ApiResponse<T>> ProcessResponse<T>(HttpResponseMessage response)
        {
            if (response == null)
                return ApiResponse<T>.Fail("Unable to make request");

            var content = await response.Content.ReadAsStringAsync();

            if (!_validStatusCodes.Contains(response.StatusCode))
                return ApiResponse<T>.Fail(content, response.StatusCode);

            return ApiResponse<T>.Success(JsonConvert.DeserializeObject<T>(content));
        }

        protected async Task<ApiResponse> ProcessResponse(HttpResponseMessage response)
        {
            if (response == null)
                return ApiResponse.Fail("Unable to make request");

            var content = await response.Content.ReadAsStringAsync();

            if (!_validStatusCodes.Contains(response.StatusCode))
                return ApiResponse.Fail(content, response.StatusCode);

            return ApiResponse.Success();
        }
    }
}
