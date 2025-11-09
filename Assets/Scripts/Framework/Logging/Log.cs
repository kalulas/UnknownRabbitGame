#region FILE HEADER

// Filename: Log.cs
// Author: Kalulas
// Create: 2025-04-13
// Description:

#endregion

using System;

namespace Framework.Logging
{
    public enum LogLevel
    {
        Debug,
        Information,
        Warning,
        Error,
        Count
    }

    public static class Log
    {
        private class LoggingConfiguration
        {
            public readonly string Prefix;

            public LoggingConfiguration(string prefix)
            {
                Prefix = prefix;
            }
        }

        #region Fields

        public const uint NoCategory = 0;

        private const string m_DebugLevelPrefix = "[DEBUG] ";
        private const string m_InfoLevelPrefix = "[INFO] ";
        private const string m_WarningLevelPrefix = "[WARNING] ";
        private const string m_ErrorLevelPrefix = "[ERROR] ";

        private static readonly LoggingConfiguration[] m_Configurations =
        {
            new(m_DebugLevelPrefix),
            new(m_InfoLevelPrefix),
            new(m_WarningLevelPrefix),
            new(m_ErrorLevelPrefix),
        };

        /// <summary>
        /// A bitmask (support 64 categories) representing enabled logging categories.
        /// </summary>
        private static ulong m_LoggingCategoryMask = ulong.MaxValue;

        private static ILogger m_Logger = new ConsoleLogger();

        #endregion

        #region Public Methods

        public static void RegisterLogger(ILogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger), "parameter cannot be null");
            }

            m_Logger = logger;
        }

        public static void SetLoggingCategoryMask(ulong categoryMask)
        {
            m_LoggingCategoryMask = categoryMask;
        }

        public static void SetLoggingLevelPrefix(LogLevel level, string prefix)
        {
            if (level < LogLevel.Debug || level >= LogLevel.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(level), "invalid log level");
            }

            m_Configurations[(int) level] = new LoggingConfiguration(prefix);
        }

        #region Logging with Levels, No Category

        public static void Debug(string content)
        {
            LogInternal(LogLevel.Debug, NoCategory, content);
        }

        public static void Info(string content)
        {
            LogInternal(LogLevel.Information, NoCategory, content);
        }

        public static void Warning(string content)
        {
            LogInternal(LogLevel.Warning, NoCategory, content);
        }

        public static void Error(string content)
        {
            LogInternal(LogLevel.Error, NoCategory, content);
        }

        public static void Debug(string content, params object[] param)
        {
            LogFmtInternal(LogLevel.Debug, NoCategory, content, param);
        }

        public static void Info(string content, params object[] param)
        {
            LogFmtInternal(LogLevel.Information, NoCategory, content, param);
        }

        public static void Warning(string content, params object[] param)
        {
            LogFmtInternal(LogLevel.Warning, NoCategory, content, param);
        }

        public static void Error(string content, params object[] param)
        {
            LogFmtInternal(LogLevel.Error, NoCategory, content, param);
        }

        #endregion

        #region Logging With Categories

        public static void Debug(ulong category, string content)
        {
            if ((m_LoggingCategoryMask & category) != 0)
            {
                LogInternal(LogLevel.Debug, category, content);
            }
        }

        public static void Info(ulong category, string content)
        {
            if ((m_LoggingCategoryMask & category) != 0)
            {
                LogInternal(LogLevel.Information, category, content);
            }
        }

        public static void Warning(ulong category, string content)
        {
            if ((m_LoggingCategoryMask & category) != 0)
            {
                LogInternal(LogLevel.Warning, category, content);
            }
        }

        public static void Error(ulong category, string content)
        {
            if ((m_LoggingCategoryMask & category) != 0)
            {
                LogInternal(LogLevel.Error, category, content);
            }
        }

        public static void Debug(ulong category, string format, params object[] args)
        {
            if ((m_LoggingCategoryMask & category) != 0)
            {
                LogFmtInternal(LogLevel.Debug, category, format, args);
            }
        }

        public static void Info(ulong category, string format, params object[] args)
        {
            if ((m_LoggingCategoryMask & category) != 0)
            {
                LogFmtInternal(LogLevel.Information, category, format, args);
            }
        }

        public static void Warning(ulong category, string format, params object[] args)
        {
            if ((m_LoggingCategoryMask & category) != 0)
            {
                LogFmtInternal(LogLevel.Warning, category, format, args);
            }
        }

        public static void Error(ulong category, string format, params object[] args)
        {
            if ((m_LoggingCategoryMask & category) != 0)
            {
                LogFmtInternal(LogLevel.Error, category, format, args);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        private static void LogFmtInternal(LogLevel level, ulong category, string format, params object[] args)
        {
            var config = m_Configurations[(int) level];
            var prefix = config.Prefix;
            m_Logger.LogFormat(level, category, prefix, format, args);
        }

        private static void LogInternal(LogLevel level, ulong category, string content)
        {
            var config = m_Configurations[(int) level];
            var prefix = config.Prefix;
            m_Logger.Log(level, category, prefix, content);
        }

        #endregion
    }
}