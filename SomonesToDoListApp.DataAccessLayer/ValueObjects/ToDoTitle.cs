using System;
using System.Collections.Generic;

namespace SomeonesToDoListApp.DataAccessLayer.ValueObjects
{
    public class ToDoTitle : ValueObject
    {
        public string Value { get; private set; }

        public ToDoTitle()
        {
        }

        public ToDoTitle(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "The title cannot be empty.");

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
