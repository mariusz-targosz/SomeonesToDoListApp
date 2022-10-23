using System;

namespace SomeonesToDoListApp.DataAccessLayer.Entities
{
    public class ToDo
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Guid CreatedBy { get; private set; }

        public ToDo()
        {
        }

        public ToDo(Guid id, string title, string description, DateTime createdAt, Guid createdBy)
        {
            Id = id;
            Title = title;
            Description = description;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
        }

        // TODO: Extract Title into VO
        public void Update(string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title), "The title cannot be empty.");

            Title = title;
            Description = description;
        }
    }
}