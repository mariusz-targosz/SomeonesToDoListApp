using System.ComponentModel.DataAnnotations;

namespace SomeonesToDoListApp.Models
{
    public class ToDoUpdateRequest
    {
        [Required]
        public string Title { get; }

        public string Description { get; }

        public ToDoUpdateRequest(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
