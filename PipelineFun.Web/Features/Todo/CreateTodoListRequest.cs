using System.Security.Principal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PipelineFun.Web.Contracts;
using PipelineFun.Web.Data.Entities;

namespace PipelineFun.Web.Features.Todo
{
    public class CreateTodoListRequest : IRequest<TodoList>, IAuthenticatedRequest
    {
        public string Title { get; set; }
        public bool IsPublic { get; set; }

        [BindNever]
        public IPrincipal User { get; set; }
    }
}