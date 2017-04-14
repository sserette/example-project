using ExampleProject.Shared.Interfaces;
using System;
using System.Diagnostics;


namespace ExampleProject.Shared
{
    public class DebugLogger : ILogger
    {
        public void Information(string message)
        {
            Debug.WriteLine(message);
        }

        public void Information(string fmt, params object[] vars)
        {
            Debug.WriteLine(fmt, vars);
        }

        public void Information(Exception exception, string fmt, params object[] vars)
        {
            var msg = String.Format(fmt, vars);
            Debug.WriteLine(string.Format(fmt, vars) + ";Exception Details={0}", ExceptionUtils.FormatException(exception, includeContext: true));
        }

        //
        // Warning - trace warnings within the application 

        public void Warning(string message)
        {
            Debug.WriteLine(message);
        }

        public void Warning(string fmt, params object[] vars)
        {
            Debug.WriteLine(fmt, vars);
        }

        public void Warning(Exception exception, string fmt, params object[] vars)
        {
            var msg = String.Format(fmt, vars);
            Debug.WriteLine(string.Format(fmt, vars) + ";Exception Details={0}", ExceptionUtils.FormatException(exception, includeContext: true));
        }

        //
        // Error - trace fatal errors within the application 

        public void Error(string message)
        {
            Debug.WriteLine(message);
        }

        public void Error(string fmt, params object[] vars)
        {
            Debug.WriteLine(fmt, vars);
        }

        public void Error(Exception exception, string fmt, params object[] vars)
        {
            var msg = String.Format(fmt, vars);
            Debug.WriteLine(string.Format(fmt, vars) + ";Exception Details={0}", ExceptionUtils.FormatException(exception, includeContext: true));
        }

        //
        // TraceAPI - trace inter-service calls (including latency)

        public void TraceApi(string assemblyName, string request, TimeSpan timespan)
        {
            TraceApi(assemblyName, request, timespan, "");
        }

        public void TraceApi(string assemblyName, string request, TimeSpan timespan, string fmt, params object[] vars)
        {
            TraceApi(assemblyName, request, timespan, string.Format(fmt, vars));
        }

        public void TraceApi(string assemblyName, string request, TimeSpan timespan, string properties)
        {
            string message = String.Concat("assembly:", assemblyName, ";request:", request, ";timespan:", timespan.ToString(), ";properties:", properties);
            Debug.WriteLine(message);
        }
    }
}
