#region FILE HEADER
// Filename: URGEventFunctionCaller.cs
// Author: Kalulas
// Create: 2022-10-31
// Description:
// Design ->
// GPP ->
#endregion

using Framework.GameScene;
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
            GameSceneManager.Instance.StartNewGame(new DemoGame());
        }

        public override void OnCallerStart()
        {
            Debug.Log("[URGEventFunctionCaller.OnCallerStart]");
            GameSceneManager.Instance.Start();
        }

        public override void OnCallerUpdate()
        {
            GameSceneManager.Instance.Update();
        }

        public override void OnCallerDestroy()
        {
            Debug.Log("[URGEventFunctionCaller.OnCallerDestroy]");
            GameSceneManager.Instance.OnDestroy();
        }
    }
}