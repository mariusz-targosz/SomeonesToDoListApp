using System;

namespace SomeonesToDoListApp.Services.Logging
{
    public interface ILogger<T> where T : class
    {
        void LogInformation(string message, params object[] args);
        void LogWarning(string message, params object[] args);
        void LogError(Exception exception, string message, params object[] args);
    }
}