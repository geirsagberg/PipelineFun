using System.ComponentModel.DataAnnotations;

namespace PipelineFun.Web.Data.Entities
{
    public class TodoItem
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public bool Done { get; set; }

        public int TodoListId { get; set; }

        public TodoList TodoList { get; set; }
    }
}