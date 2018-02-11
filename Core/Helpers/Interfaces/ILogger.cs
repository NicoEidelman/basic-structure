using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers.Interfaces
{
    public interface ILogger
    {
        void LogException(Exception ex, bool reThrow = false);

        void LogMessage(string message);
    }
}
