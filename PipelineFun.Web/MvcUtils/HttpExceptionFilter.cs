using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PipelineFun.Web.MvcUtils
{
    public class HttpExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (!(context.Exception is HttpException httpException))
                return;

            context.HttpContext.Response.StatusCode = httpException.StatusCodeInt;

            var responseFeature = context.HttpContext.Features.Get<IHttpResponseFeature>();
            responseFeature.ReasonPhrase = httpException.Message;
        }
    }
}