using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF6CodeFirstApporach.Logging
{
    public interface ILogger
    {
         void Information(string message);
         void Information(string format, params object[] args);
         void Information(Exception ex,string format, params object[] args);

        void Warning(Exception ex,string format, params object[] args);
        void Warning(string format, params object[] args);
        void Warning(string message);

        void Error(Exception ex, string format, params object[] args);
        void Error(string format, params object[] args);
        void Error(string message);

        // The TraceApi methods are for logging external service calls with information about latency.You could also add a set of methods for Debug/Verbose level.
        void TraceApi(string componentName, string method, TimeSpan timespan);
        void TraceApi(string componentName, string method, TimeSpan timespan, string properties);
        void TraceApi(string componentName, string method, TimeSpan timespan, string fmt, params object[] vars);
    }
}
