using System;
using NLog;

namespace SomeonesToDoListApp.Services.Logging
{
    public class NLogger<T> : ILogger<T> where T : class
    {
        private readonly Logger _logger;

        public NLogger()
        {
            _logger = LogManager.GetLogger(nameof(T));
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.Warn(message, args);
        }

        public void LogError(Exception exception, string message, params object[] args)
        {
            _logger.Error(exception, message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.Info(message, args);
        }
    }
}