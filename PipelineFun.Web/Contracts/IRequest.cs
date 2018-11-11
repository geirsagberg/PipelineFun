namespace PipelineFun.Web.Contracts
{
    public interface IRequestBase
    {
    }

    public interface IRequest : IRequestBase
    {
    }

    public interface IRequest<out TResult> : IRequestBase
    {
    }
}