using System;

namespace SomeonesToDoListApp.Services.Services
{
    public interface IDateTimeProvider
    {
        DateTime NowUtc { get; }
    }

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}