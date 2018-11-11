using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PipelineFun.Web.Data.Entities;

namespace PipelineFun.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> Todos { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }
    }
}