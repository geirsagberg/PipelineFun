using System.Threading.Tasks;

namespace PipelineFun.Web.Contracts
{
    public interface IRequestHandler {}

    public interface IRequestHandler<in TRequest> : IRequestHandler where TRequest : IRequest
    {
        Task Handle(TRequest request);
    }

    public interface IRequestHandler<in TRequest, TResult> : IRequestHandler where TRequest : IRequest<TResult>
    {
        Task<TResult> Handle(TRequest request);
    }

}