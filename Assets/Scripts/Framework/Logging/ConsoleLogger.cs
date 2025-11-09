#region FILE HEADER

// Filename: ConsoleLogger.cs
// Author: Kalulas
// Create: 2025-11-09
// Description:

#endregion

using System;

namespace Framework.Logging
{
    /// <summary>
    /// The default / fallback logger using <see cref="System.Console.WriteLine(string)"/>
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        public void Log(LogLevel logLevel, ulong category, string prefix, string content)
        {
            Console.WriteLine(prefix + content);
        }

        public void LogFormat(LogLevel logLevel, ulong category, string prefix, string format, params object[] args)
        {
            Console.WriteLine(prefix + format, args);
        }
    }
}