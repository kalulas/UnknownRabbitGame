#region FILE HEADER

// Filename: UnityEngineDebugLogger.cs
// Author: Kalulas
// Create: 2025-11-09
// Description:

#endregion

using System;
using Framework.Logging;

namespace UnityBasedFramework.Logging
{
    public class UnityEngineDebugLogger : ILogger
    {
        public void Log(LogLevel logLevel, ulong category, string prefix, string content)
        {
            var message = prefix + content;
            switch (logLevel)
            {
                case LogLevel.Debug:
                case LogLevel.Information:
                    UnityEngine.Debug.Log(message);
                    break;
                case LogLevel.Warning:
                    UnityEngine.Debug.LogWarning(message);
                    break;
                case LogLevel.Error:
                    UnityEngine.Debug.LogError(message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }
        }

        public void LogFormat(LogLevel logLevel, ulong category, string prefix, string format, params object[] args)
        {
            var message = prefix + format;
            switch (logLevel)
            {
                case LogLevel.Debug:
                case LogLevel.Information:
                    UnityEngine.Debug.LogFormat(message, args);
                    break;
                case LogLevel.Warning:
                    UnityEngine.Debug.LogWarningFormat(message, args);
                    break;
                case LogLevel.Error:
                    UnityEngine.Debug.LogErrorFormat(message, args);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }
        }
    }
}