#region FILE HEADER

// Filename: URGEntry.cs
// Author: Kalulas
// Create: 2022-10-31
// Description:

#endregion

using Framework.Logging;
using UnityBasedFramework.GameScene;
using UnknownRabbitGame.Logging;

namespace UnknownRabbitGame.Basic
{
    public class URGEntry : Entry
    {
        private static void RegisterLoggers()
        {
            Log.RegisterLogger(new UnknownRabbitGameLogger());
        }

        /// <summary>
        /// This is the entry of the UnknownRabbitGame
        /// </summary>
        public override void OnEntryAwake()
        {
            RegisterLoggers();
            Log.Debug((ulong) Category.Entry, "EntryAwake!");
            gameObject.AddComponent<URGEventFunctionCaller>();
        }
    }
}