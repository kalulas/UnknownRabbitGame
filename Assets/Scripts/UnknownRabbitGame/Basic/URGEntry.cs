#region FILE HEADER
// Filename: URGEntry.cs
// Author: Kalulas
// Create: 2022-10-31
// Description:
// Design ->
// GPP ->
#endregion

using UnityBasedFramework.GameScene;
using UnityEngine;

namespace UnknownRabbitGame.Basic
{
    public class URGEntry : Entry
    {
        public override void OnEntryAwake()
        {
            Debug.Log("[URGEntry.OnEntryAwake]");
            gameObject.AddComponent<URGEventFunctionCaller>();
        }
    }
}