#region FILE HEADER
// Filename: LauncherGame.cs
// Author: Kalulas
// Create: 2022-11-27
// Description: This is the BaseGame class for Launcher scene
#endregion

using System;
using System.Threading.Tasks;
using Framework.Debug;
using Framework.GameScene;
using UnityBasedFramework.GameScene;
using UnityEngine;
using UnknownRabbitGame.Basic;

namespace UnknownRabbitGame.GameScene
{
    public class LauncherGame : BaseGame
    {
        #region Fields

        private const float m_FrameLength = 0.01f;

        #endregion
        
        #region BaseGame

        protected override void OnGameUpdate(float deltaTime)
        {
            
        }

        protected override void OnGameFrameUpdate(uint frameCount)
        {
            
        }

        protected override void OnGameStart()
        {
            Log.Debug("[LauncherGame] start!");
            WaitAndLoadNextGame();
        }

        protected override void OnGameSceneReady()
        {
            
        }

        protected override void OnGameExit()
        {
            Log.Debug("[LauncherGame] exit!");
        }

        public override float GetFrameLength()
        {
            return m_FrameLength;
        }

        #endregion

        #region Private Utils

        private async void WaitAndLoadNextGame()
        {
            await Task.Delay(new TimeSpan(0, 0, 2)); // wait for 5 seconds
            // await GameSceneManager.Instance.StartNewGameWithScene<DemoGame>("Scenes/UnknownRabbitScene");
            GameSceneManager.Instance.StartNewGameWithSceneCoroutine<DemoGame>("Scenes/UnknownRabbitScene",
                URGEventFunctionCaller.Instance);
        }

        #endregion
    }
}