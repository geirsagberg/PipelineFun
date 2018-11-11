using System.Threading.Tasks;

namespace PipelineFun.Web.Contracts
{
    public interface IRequestPreProcessor<in TRequest>
    {
        Task ProcessRequestAsync(TRequest request);
    }
}