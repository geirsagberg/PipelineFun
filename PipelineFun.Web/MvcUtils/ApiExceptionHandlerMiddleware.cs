using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PipelineFun.Web.Extensions;

namespace PipelineFun.Web.MvcUtils
{
    public class ApiExceptionHandlerMiddleware
    {
        private readonly ILogger<ApiExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ApiExceptionHandlerMiddleware(RequestDelegate next, ILogger<ApiExceptionHandlerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try {
                await next(context);
            } catch (Exception ex) {
                var logLevel = ex is HttpException httpException && httpException.StatusCodeInt.InRange(400, 499)
                    ? LogLevel.Warning
                    : LogLevel.Error;
                logger.Log(logLevel, ex, "An unhandled exception has occurred while executing the request");
                if (context.Response.HasStarted) {
                    logger.LogWarning(
                        "The response has already started, the API error middleware will not be executed.");
                    throw;
                }

                try {
                    context.Response.Clear();
                    context.Response.StatusCode = ex is HttpException httpExecption ? httpExecption.StatusCodeInt : 500;
                    context.Response.Headers.Add("content-type", "application/json");
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new {
                        error = new {
                            message = ex.Message
                        }
                    }));
                } catch (Exception ex2) {
                    logger.LogError(ex2, "An exception was thrown attempting to return an API error response.");
                    throw;
                }
            }
        }
    }
}