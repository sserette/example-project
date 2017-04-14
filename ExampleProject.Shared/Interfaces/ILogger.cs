using System;

namespace ExampleProject.Shared.Interfaces
{
    public interface ILogger
    {
        void Information(string message);
        void Information(string fmt, params object[] vars);
        void Information(Exception exception, string fmt, params object[] vars);

        void Warning(string message);
        void Warning(string fmt, params object[] vars);
        void Warning(Exception exception, string fmt, params object[] vars);

        void Error(string message);
        void Error(string fmt, params object[] vars);
        void Error(Exception exception, string fmt, params object[] vars);

        void TraceApi(string assemblyName, string request, TimeSpan timespan);
        void TraceApi(string assemblyName, string request, TimeSpan timespan, string properties);
        void TraceApi(string assemblyName, string request, TimeSpan timespan, string fmt, params object[] vars);
    }
}
