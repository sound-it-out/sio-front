using System.Net;

namespace SIO.Domain
{
    public record ApiResponse<T>
    {
        public T Result { get; init; }
        public HttpStatusCode? StatusCode { get; init; }
        public string Error { get; init; }
        public bool IsError { get; init; }

        private ApiResponse(T result)
        {
            Result = result;
        }

        private ApiResponse(string error, HttpStatusCode? statusCode)
        {
            Error = error;
            StatusCode = statusCode;
            IsError = true;
        }

        public static ApiResponse<T> Success(T result) => new ApiResponse<T>(result);
        public static ApiResponse<T> Fail(string error, HttpStatusCode? statusCode = null) => new ApiResponse<T>(error, statusCode);
    }

    public record ApiResponse
    {
        public HttpStatusCode? StatusCode { get; init; }
        public string Error { get; init; }
        public bool IsError { get; init; }

        private ApiResponse() { }

        private ApiResponse(string error, HttpStatusCode? statusCode)
        {
            Error = error;
            StatusCode = statusCode;
            IsError = true;
        }

        public static ApiResponse Success() => new ApiResponse();
        public static ApiResponse Fail(string error, HttpStatusCode? statusCode = null) => new ApiResponse(error, statusCode);
    }
}
