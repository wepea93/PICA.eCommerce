using Microsoft.Extensions.Logging;

namespace eCommerce.Orders.Core.Helpers.Log
{
    public class LogHelper : ILogHelper
    {
        private readonly ILogger<LogHelper> _logger;

        public LogHelper(ILogger<LogHelper> logger)
        {
            _logger = logger;
        }

        public async Task RegisterLog(string? message, params object?[] args)
        {
            _logger.LogError(message, args);
        }
    }
}
