namespace SomeonesToDoListApp.Requests
{
    public class ToDoUpdateRequest
    {
        public string Title { get; }
        public string Description { get; }

        public ToDoUpdateRequest(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
