using System;

namespace SomeonesToDoListApp.Models
{
    public class ToDoResponse
    {
        public Guid Id { get; }
        public string Title { get; }
        public string Description { get; }

        public ToDoResponse(Guid id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }
    }
}