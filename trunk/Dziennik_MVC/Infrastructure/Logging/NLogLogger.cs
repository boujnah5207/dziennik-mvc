using NLog;

namespace Dziennik_MVC.Infrastructure.Logging
{
    public class NLogLogger :ILogger
    {
        private Logger _logger;

        public NLogLogger()
        {

           _logger = LogManager.GetCurrentClassLogger();
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }
    }
}