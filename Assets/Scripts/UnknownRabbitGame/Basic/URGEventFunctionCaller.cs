#region FILE HEADER
// Filename: URGEventFunctionCaller.cs
// Author: Kalulas
// Create: 2022-10-31
// Description:
// Design ->
// GPP ->
#endregion

using UnityBasedFramework.GameScene;
using UnityEngine;
using UnknownRabbitGame.GameScene;

namespace UnknownRabbitGame.Basic
{
    public class URGEventFunctionCaller : EventFunctionCaller
    {
        public override void OnCallerAwake()
        {
            Debug.Log("[URGEventFunctionCaller.OnCallerAwake]");
            GameSceneManager.Instance.StartNewGame<LauncherGame>();
        }

        public override void OnCallerStart()
        {
            Debug.Log("[URGEventFunctionCaller.OnCallerStart]");
            // GameSceneManager.Instance.OnCallerStart();
        }

        public override void OnCallerUpdate()
        {
            GameSceneManager.Instance.OnCallerUpdate();
        }

        public override void OnCallerDestroy()
        {
            Debug.Log("[URGEventFunctionCaller.OnCallerDestroy]");
            // GameSceneManager.Instance.OnCallerDestroy();
        }
    }
}