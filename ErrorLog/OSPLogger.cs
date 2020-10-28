using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneStopApp_Api.EntityFramework.Data;
using OneStopApp_Api.EntityFramework.Model;

namespace Dms.Services.Implementation.ErrorLog
{
    public class OSPLogger : ILogger
    {
        private string _categoryName;
        private Func<string, LogLevel, bool> _filter;
        private string _connectionString;
        public OSPLogger(string categoryName, Func<string, LogLevel, bool> filter, string connectionString)
        {
            _categoryName = categoryName;
            _filter = filter;
            _connectionString = connectionString;
        }
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }
            var message = formatter(state, exception);
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            if (exception != null)
            {
                message += "\n" + exception.ToString();
            }
            var eventLog = new EventLog
            {
                Message = message,
                EventId = eventId.Id,
                LogLevel = logLevel.ToString(),
                CreatedTime = DateTime.UtcNow
            };

            var options = new DbContextOptionsBuilder<OsaLogContext>()
            .UseSqlServer(_connectionString)
            .Options;
            using (var dbContext = new OsaLogContext(options))
            {
                dbContext.EventLogs.Add(eventLog);
                try
                {
                    dbContext.SaveChanges();
                }
                catch
                { }
            }
        }
        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == LogLevel.Error || logLevel == LogLevel.Critical;
            // return (_filter == null || _filter(_categoryName, logLevel));
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}