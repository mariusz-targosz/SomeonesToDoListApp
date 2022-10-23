using System;

namespace SomeonesToDoListApp.ViewModels
{
    public class ToDoViewModel
    {
        public Guid Id { get; }
        public string Title { get; }
        public string Description { get; }

        public ToDoViewModel(Guid id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }
    }
}