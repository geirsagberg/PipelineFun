using System;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using PipelineFun.Web.Contracts;

namespace PipelineFun.Web.MvcUtils
{
    public class PipelineActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var value in context.ActionArguments.Values) {
                var processorType = typeof(IRequestPreProcessor<>).MakeGenericType(value.GetType());
                object processor;
                try {
                    processor = context.HttpContext.RequestServices.GetService(processorType);
                } catch (ArgumentException) {
                    processor = null;
                }

                if (processor != null) {
                    try {
                        var task = (Task) processorType
                            .GetMethod(nameof(IRequestPreProcessor<object>.ProcessRequestAsync))
                            .Invoke(processor, new[] {value});
                        await task;
                    } catch (TargetInvocationException ex) {
                        ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                    }
                }
            }

            await next();
        }
    }
}