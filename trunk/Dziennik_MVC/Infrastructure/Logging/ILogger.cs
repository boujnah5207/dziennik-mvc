using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dziennik_MVC.Infrastructure.Logging
{
    public interface ILogger
    {
        void Info(string message);
    }
}
