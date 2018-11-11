using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PipelineFun.Web.Data.Entities
{
    public class TodoList
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string CreatedBy { get; set; }
        
        public bool IsPublic { get; set; }

        public ICollection<TodoItem> Todos { get; set; } = new List<TodoItem>();
    }
}