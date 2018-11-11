using System;
using System.Net;

namespace PipelineFun.Web.MvcUtils
{
    public class HttpException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public int StatusCodeInt => (int) StatusCode;

        public HttpException(int httpStatusCode)
        {
            StatusCode = (HttpStatusCode) httpStatusCode;
        }

        public HttpException(HttpStatusCode httpStatusCode)
        {
            StatusCode = httpStatusCode;
        }

        public HttpException(int httpStatusCode, string message) : base(message)
        {
            StatusCode = (HttpStatusCode) httpStatusCode;
        }

        public HttpException(HttpStatusCode httpStatusCode, string message) : base(message)
        {
            StatusCode = httpStatusCode;
        }

        public HttpException(int httpStatusCode, string message, Exception inner) : base(message, inner)
        {
            StatusCode = (HttpStatusCode) httpStatusCode;
        }

        public HttpException(HttpStatusCode httpStatusCode, string message, Exception inner) : base(message, inner)
        {
            StatusCode = httpStatusCode;
        }
    }
}