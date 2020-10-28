using System;
using Dms.Services.Implementation.ErrorLog;
using Microsoft.Extensions.Logging;

namespace OneStopApp_Api.ErrorLog
{
    public class OneStopAppLoggerProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, bool> _filter;
        private string _connectionString;

        public OneStopAppLoggerProvider(Func<string, LogLevel, bool> filter, string connectionString)
        {
            _filter = filter;
            _connectionString = connectionString;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new OSPLogger(categoryName, _filter, _connectionString);
        }
        public void Dispose()
        {
        }
    }
}