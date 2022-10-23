using System.ComponentModel.DataAnnotations;

namespace SomeonesToDoListApp.Requests
{
    public class ToDoAddRequest
    {
        [Required]
        public string Title { get; }

        public string Description { get; }

        public ToDoAddRequest(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}