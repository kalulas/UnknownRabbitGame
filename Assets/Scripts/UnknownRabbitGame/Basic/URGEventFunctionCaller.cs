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
            GameSceneManager.Instance.StartNewGame<LauncherGame>();
        }

        public override void OnCallerStart()
        {
            // GameSceneManager.Instance.OnCallerStart();
        }

        public override void OnCallerUpdate(float deltaTime)
        {
            GameSceneManager.Instance.OnCallerUpdate(deltaTime);
        }

        public override void OnCallerFixedUpdate(float fixedDeltaTime)
        {
            GameSceneManager.Instance.OnCallerFixedUpdate(fixedDeltaTime);
        }

        public override void OnCallerDestroy()
        {
            // GameSceneManager.Instance.OnCallerDestroy();
        }
    }
}