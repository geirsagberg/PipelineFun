using System.Security.Principal;

namespace PipelineFun.Web.Contracts
{
    public interface IAuthenticatedRequest
    {
        IPrincipal User { get; set; }
    }
}