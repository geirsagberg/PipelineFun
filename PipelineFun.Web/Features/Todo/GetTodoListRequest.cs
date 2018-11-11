using PipelineFun.Web.Contracts;
using PipelineFun.Web.Data.Entities;

namespace PipelineFun.Web.Features.Todo
{
    public class GetTodoListRequest : IRequest<TodoList>
    {
        public int ListId { get; set; }
    }
}