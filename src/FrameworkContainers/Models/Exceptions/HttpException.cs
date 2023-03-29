using FrameworkContainers.Network;
using System;

namespace FrameworkContainers.Models.Exceptions
{
    public sealed class HttpException : Exception
    {
        public int StatusCode { get; }
        public string Body { get; }
        public Header[] Headers { get; }

        public HttpException(string message, int statusCode, string body, Exception ex, Header[] headers) : base(message, ex)
        {
            StatusCode = statusCode;
            Body = body;
            Headers = headers; 
        }
    }
}
