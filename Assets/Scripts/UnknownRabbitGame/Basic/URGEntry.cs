#region FILE HEADER

// Filename: URGEntry.cs
// Author: Kalulas
// Create: 2022-10-31
// Description:

#endregion

using Framework.Debug;
using UnityBasedFramework.GameScene;
using UnityEngine;

namespace UnknownRabbitGame.Basic
{
    public class URGEntry : Entry
    {
        private static void RegisterLoggers()
        {
            Log.RegisterLogger(Log.LogLevel.Debug, Debug.Log);
            Log.RegisterLogger(Log.LogLevel.Information, Debug.Log);
            Log.RegisterLogger(Log.LogLevel.Warning, Debug.LogWarning);
            Log.RegisterLogger(Log.LogLevel.Error, Debug.LogError);

            Log.RegisterFmtLogger(Log.LogLevel.Debug, Debug.LogFormat);
            Log.RegisterFmtLogger(Log.LogLevel.Information, Debug.LogFormat);
            Log.RegisterFmtLogger(Log.LogLevel.Warning, Debug.LogWarningFormat);
            Log.RegisterFmtLogger(Log.LogLevel.Error, Debug.LogErrorFormat);
        }

        /// <summary>
        /// This is the entry of the UnknownRabbitGame
        /// </summary>
        public override void OnEntryAwake()
        {
            RegisterLoggers();
            Log.Debug("[URGEntry] EntryAwake!");
            gameObject.AddComponent<URGEventFunctionCaller>();
        }
    }
}