using System.Collections.Generic;
using PipelineFun.Web.Contracts;
using PipelineFun.Web.Data.Entities;

namespace PipelineFun.Web.Features.Todo
{
    public class GetTodoListsRequest : IRequest<IList<TodoList>>
    {
    }
}