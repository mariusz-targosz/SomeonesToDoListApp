using System;

namespace SomeonesToDoListApp.Services
{
    public interface ICurrentUserService
    {
        string UserId { get; }
    }

    public class HttpContextCurrentUserService : ICurrentUserService
    {
        // TODO: Get User ID from HttpContext
        public string UserId { get; } = Guid.Empty.ToString();
    }
}