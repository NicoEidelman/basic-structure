using Core.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;


namespace Core.Helpers
{
    public class Logger : ILogger
    {
        public void LogException(Exception ex, bool reThrow = false)
        {
            var client = new TelemetryClient();
            client.TrackException(ex);

            if (reThrow)
            {
                throw ex;
            }
        }

        public void LogMessage(string message)
        {
            var client = new TelemetryClient();
            client.TrackTrace(message);
        }
    }
}
