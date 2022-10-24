using System;
using SomeonesToDoListApp.DataAccessLayer.ValueObjects;

namespace SomeonesToDoListApp.DataAccessLayer.Entities
{
    public class ToDo
    {
        public Guid Id { get; private set; }
        public ToDoTitle Title { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string CreatedBy { get; private set; }

        public ToDo()
        {
        }

        public ToDo(Guid id, ToDoTitle title, string description, DateTime createdAt, string createdBy)
        {
            Id = id;
            Title = title;
            Description = description;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
        }

        public void Update(ToDoTitle title, string description)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description;
        }
    }
}