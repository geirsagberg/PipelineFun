using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PipelineFun.Web.Contracts;
using PipelineFun.Web.Data.Entities;

namespace PipelineFun.Web.Features.Todo
{
    [ApiController]
    public class TodoListController : Controller
    {
        [HttpGet("TodoLists")]
        public Task<IList<TodoList>> GetLists(IRequestHandler<GetTodoListsRequest, IList<TodoList>> handler)
        {
            return handler.Handle(new GetTodoListsRequest());
        }

        [HttpGet("TodoLists/{listId:int}")]
        public Task<TodoList> GetLists(int listId, IRequestHandler<GetTodoListRequest, TodoList> handler)
        {
            return handler.Handle(new GetTodoListRequest {
                ListId = listId
            });
        }

        [HttpPost("TodoLists")]
        public Task<TodoList> CreateList(CreateTodoListRequest request,
            IRequestHandler<CreateTodoListRequest, TodoList> handler)
        {
            return handler.Handle(request);
        }

        [HttpPost("TodoLists/{listId:int}/Items")]
        public Task<TodoItem> CreateTodo(int listId, CreateTodoItemRequest request,
            IRequestHandler<CreateTodoItemRequest, TodoItem> handler)
        {
            request.ListId = listId;
            return handler.Handle(request);
        }

        [HttpGet("TodoLists/{listId:int}/Items")]
        public Task<IList<TodoItem>> GetTodos(int listId, IRequestHandler<GetTodoItemsRequest, IList<TodoItem>> handler)
        {
            return handler.Handle(new GetTodoItemsRequest {
                ListId = listId
            });
        }
    }
}
