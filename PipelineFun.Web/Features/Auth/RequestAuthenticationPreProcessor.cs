using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PipelineFun.Web.Contracts;
using PipelineFun.Web.MvcUtils;

namespace PipelineFun.Web.Features.Auth
{
    public class RequestAuthenticationPreProcessor<T> : IRequestPreProcessor<T> where T : IAuthenticatedRequest
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public RequestAuthenticationPreProcessor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task ProcessRequestAsync(T request)
        {
            var user = httpContextAccessor.HttpContext.User;

            if (!user.Identity.IsAuthenticated) {
                throw new HttpException(HttpStatusCode.Unauthorized,
                    "Only authenticated users can perform this action");
            }

            request.User = user;
            return Task.CompletedTask;
        }
    }
}