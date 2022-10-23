using System;
using SomeonesToDoListApp.DataAccessLayer.Entities;

namespace SomeonesToDoListApp.Services.Services
{
    public interface IToDoFactory
    {
        ToDo Create(string title, string description, Guid createdBy);
    }

    public class ToDoFactory : IToDoFactory
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public ToDoFactory(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public ToDo Create(string title, string description, Guid createdBy)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title), "The title cannot be empty.");

            if (createdBy == Guid.Empty)
                throw new ArgumentNullException(nameof(createdBy), "The created by cannot be empty.");

            var now = _dateTimeProvider.NowUtc;
            return new ToDo(Guid.NewGuid(), title, description, now, createdBy);
        }
    }
}