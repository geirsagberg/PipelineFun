using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PipelineFun.Web.Contracts;
using PipelineFun.Web.Data;
using PipelineFun.Web.Data.Entities;

namespace PipelineFun.Web.Features.Todo
{
    public class TodoHandler :
        IRequestHandler<GetTodoListsRequest, IList<TodoList>>,
        IRequestHandler<CreateTodoListRequest, TodoList>,
        IRequestHandler<CreateTodoItemRequest, TodoItem>,
        IRequestHandler<GetTodoItemsRequest, IList<TodoItem>>,
        IRequestHandler<GetTodoListRequest, TodoList>
    {
        private readonly ApplicationDbContext context;

        public TodoHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<TodoItem> Handle(CreateTodoItemRequest request)
        {
            var todoItem = new TodoItem {
                Title = request.Title,
                TodoListId = request.ListId
            };
            context.Add(todoItem);
            await context.SaveChangesAsync();
            return todoItem;
        }

        public async Task<TodoList> Handle(CreateTodoListRequest request)
        {
            var todoList = new TodoList {
                Title = request.Title,
                CreatedBy = request.User.Identity.Name
            };
            context.Add(todoList);
            await context.SaveChangesAsync();
            return todoList;
        }

        public async Task<IList<TodoItem>> Handle(GetTodoItemsRequest request)
        {
            return await context.Todos.Where(t => t.TodoListId == request.ListId).ToListAsync();
        }

        public Task<TodoList> Handle(GetTodoListRequest request)
        {
            return context.FindAsync<TodoList>(request.ListId);
        }

        public async Task<IList<TodoList>> Handle(GetTodoListsRequest request)
        {
            return await context.Set<TodoList>().ToListAsync();
        }
    }
}