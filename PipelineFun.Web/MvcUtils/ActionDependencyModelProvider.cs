using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PipelineFun.Web.Contracts;

namespace PipelineFun.Web.MvcUtils
{
    public class ActionDependencyModelProvider : IApplicationModelProvider
    {
        // Run before ApiControllerAttribute
        public int Order => -901;

        public void OnProvidersExecuted(ApplicationModelProviderContext context)
        {
        }

        public void OnProvidersExecuting(ApplicationModelProviderContext context)
        {
            foreach (var controllerModel in context.Result.Controllers) {
                foreach (var actionModel in controllerModel.Actions) {
                    foreach (var parameterModel in actionModel.Parameters) {
                        if (typeof(IRequestHandler).IsAssignableFrom(parameterModel.ParameterType)) {
                            parameterModel.BindingInfo = new BindingInfo {
                                BindingSource = BindingSource.Services
                            };
                        }
                    }
                }
            }
        }
    }
}