#region FILE HEADER

// Filename: Log.cs
// Author: Kalulas
// Create: 2025-04-13
// Description:

#endregion

using System;

namespace Framework.Debug
{
    public static class Log
    {
        public enum LogLevel
        {
            Debug,
            Information,
            Warning,
            Error,
            Count
        }

        public delegate void FormatLogging(string format, params object[] param);

        public delegate void Logging(string content);

        private class LoggingConfiguration
        {
            public readonly string Prefix;
            public FormatLogging FmtLogger;
            public Logging Logger;

            public LoggingConfiguration(string prefix)
            {
                Prefix = prefix;
            }
        }

        #region Fields

        private const string DEBUG_IDENTIFIER = "[DEBUG] ";
        private const string INFO_IDENTIFIER = "[INFO] ";
        private const string WARNING_IDENTIFIER = "[WARNING] ";
        private const string ERROR_IDENTIFIER = "[ERROR] ";

        private static readonly FormatLogging m_FallbackFmtLogger = Console.WriteLine;
        private static readonly Logging m_FallbackLogger = Console.WriteLine;

        private static readonly LoggingConfiguration[] m_Configurations =
        {
            new LoggingConfiguration(DEBUG_IDENTIFIER),
            new LoggingConfiguration(INFO_IDENTIFIER),
            new LoggingConfiguration(WARNING_IDENTIFIER),
            new LoggingConfiguration(ERROR_IDENTIFIER),
        };

        #endregion

        #region Public Methods

        public static void RegisterAllLoggers(Logging logging)
        {
            if (logging == null)
            {
                throw new ArgumentNullException(nameof(logging), "parameter cannot be null");
            }

            foreach (var configuration in m_Configurations)
            {
                configuration.Logger = logging;
            }
        }

        public static void RegisterAllFmtLoggers(FormatLogging logging)
        {
            if (logging == null)
            {
                throw new ArgumentNullException(nameof(logging), "parameter cannot be null");
            }

            foreach (var configuration in m_Configurations)
            {
                configuration.FmtLogger = logging;
            }
        }

        public static void RegisterLogger(LogLevel level, Logging logging)
        {
            if (logging == null)
            {
                throw new ArgumentNullException(nameof(logging), "parameter cannot be null");
            }

            if (level >= LogLevel.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }

            m_Configurations[(int) level].Logger = logging;
        }

        public static void RegisterFmtLogger(LogLevel level, FormatLogging logging)
        {
            if (logging == null)
            {
                throw new ArgumentNullException(nameof(logging), "parameter cannot be null");
            }

            if (level >= LogLevel.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }

            m_Configurations[(int) level].FmtLogger = logging;
        }

        public static void Debug(string content)
        {
            LogInternal(LogLevel.Debug, content);
        }

        public static void Info(string content)
        {
            LogInternal(LogLevel.Information, content);
        }

        public static void Warning(string content)
        {
            LogInternal(LogLevel.Warning, content);
        }

        public static void Error(string content)
        {
            LogInternal(LogLevel.Error, content);
        }

        public static void Debug(string content, params object[] param)
        {
            LogFmtInternal(LogLevel.Debug, content, param);
        }

        public static void Info(string content, params object[] param)
        {
            LogFmtInternal(LogLevel.Information, content, param);
        }

        public static void Warning(string content, params object[] param)
        {
            LogFmtInternal(LogLevel.Warning, content, param);
        }

        public static void Error(string content, params object[] param)
        {
            LogFmtInternal(LogLevel.Error, content, param);
        }

        #endregion

        #region Private Methods

        private static void LogFmtInternal(LogLevel level, string format, params object[] param)
        {
            var config = m_Configurations[(int) level];
            var prefix = config.Prefix;
            var formatLogger = m_Configurations[(int) level].FmtLogger ?? m_FallbackFmtLogger;
            formatLogger(prefix + format, param);
        }

        private static void LogInternal(LogLevel level, string content)
        {
            var config = m_Configurations[(int) level];
            var prefix = config.Prefix;
            var formatLogger = m_Configurations[(int) level].Logger ?? m_FallbackLogger;
            formatLogger(prefix + content);
        }

        #endregion
    }
}