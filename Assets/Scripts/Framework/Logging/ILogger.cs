#region FILE HEADER

// Filename: ILogger.cs
// Author: Kalulas
// Create: 2025-11-09
// Description:

#endregion

namespace Framework.Logging
{
    public interface ILogger
    {
        void Log(LogLevel logLevel, ulong category, string prefix, string content);
        void LogFormat(LogLevel logLevel, ulong category, string prefix, string format, params object[] args);
    }
}