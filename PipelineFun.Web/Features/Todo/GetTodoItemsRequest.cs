using System.Collections.Generic;
using PipelineFun.Web.Contracts;
using PipelineFun.Web.Data.Entities;

namespace PipelineFun.Web.Features.Todo
{
    public class GetTodoItemsRequest : IRequest<IList<TodoItem>>
    {
        public int ListId { get; set; }
    }
}