using System;

namespace SomeonesToDoListApp.Requests
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