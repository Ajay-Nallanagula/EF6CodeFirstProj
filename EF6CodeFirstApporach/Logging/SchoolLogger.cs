using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;

namespace EF6CodeFirstApporach.Logging
{
    public class SchoolLogger : ILogger
    {
        public void Error(Exception ex, string format, params object[] args)
        {
            Trace.TraceError(FormattedException(ex), format, args);
        }

        private string FormattedException(Exception ex)
        {
            var sb = new StringBuilder();
            sb.Append($"Exception:{ex.ToString()}").AppendLine();
            return sb.ToString();
        }

        public void Error(string format, params object[] args)
        {
            Trace.TraceError(format, args);
        }

        public void Error(string message)
        {
            Trace.TraceError(message);
        }

        public void Information(string message)
        {
            Trace.TraceInformation(message);
        }

        public void Information(string format, params object[] args)
        {
            Trace.TraceInformation(format, args);
        }

        public void Information(Exception ex, string format, params object[] args)
        {
            Trace.TraceInformation(FormattedException(ex), format,args);
        }

        public void TraceApi(string componentName, string method, TimeSpan timespan)
        {
            TraceApi(componentName, method, timespan, "");
        }

        public void TraceApi(string componentName, string method, TimeSpan timespan, string fmt, params object[] vars)
        {
            TraceApi(componentName, method, timespan, string.Format(fmt, vars));
        }
        public void TraceApi(string componentName, string method, TimeSpan timespan, string properties)
        {
            string message = String.Concat("Component:", componentName, ";Method:", method, ";Timespan:", timespan.ToString(), ";Properties:", properties);
            Trace.TraceInformation(message);
        }

        public void Warning(Exception ex, string format, params object[] args)
        {
            Trace.TraceWarning(FormattedException(ex), format, args);
        }

        public void Warning(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Warning(string message)
        {
            throw new NotImplementedException();
        }
       
    }
}