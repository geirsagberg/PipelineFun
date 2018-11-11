using System.Security.Principal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PipelineFun.Web.Contracts;
using PipelineFun.Web.Data.Entities;

namespace PipelineFun.Web.Features.Todo
{
    public class CreateTodoItemRequest : IRequest<TodoItem>, IAuthenticatedRequest
    {
        public int ListId { get; set; }
        public string Title { get; set; }

        [BindNever]
        public IPrincipal User { get; set; }
    }
}