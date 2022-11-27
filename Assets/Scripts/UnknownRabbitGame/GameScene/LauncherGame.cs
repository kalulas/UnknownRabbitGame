#region FILE HEADER
// Filename: LauncherGame.cs
// Author: Kalulas
// Create: 2022-11-27
// Description: This is the BaseGame class for Launcher scene
#endregion

using System;
using System.Threading.Tasks;
using Framework.GameScene;
using UnityBasedFramework.GameScene;
using UnityEngine;
using UnknownRabbitGame.Basic;

namespace UnknownRabbitGame.GameScene
{
    public class LauncherGame : BaseGame
    {
        #region BaseGame

        protected override void OnGameUpdate()
        {
            
        }

        protected override void OnGameStart()
        {
            Debug.Log("[LauncherGame.OnGameStart]");
            WaitAndLoadNextGame();
        }

        protected override void OnGameSceneReady()
        {
            
        }

        protected override void OnGameExit()
        {
            Debug.Log("[LauncherGame.OnGameExit]");
        }

        #endregion

        #region Private Utils

        private async void WaitAndLoadNextGame()
        {
            await Task.Delay(new TimeSpan(0, 0, 5)); // wait for 5 seconds
            // await GameSceneManager.Instance.StartNewGameWithScene<DemoGame>("Scenes/UnknownRabbitScene");
            // Debug.Log("[LauncherGame.WaitAndLoadNextGame] Scenes/UnknownRabbitScene loaded!");
            GameSceneManager.Instance.StartNewGameWithSceneCoroutine<DemoGame>("Scenes/UnknownRabbitScene",
                URGEventFunctionCaller.Instance);
            // Debug.Log("[LauncherGame.WaitAndLoadNextGame] Scenes/UnknownRabbitScene loaded!");
        }

        #endregion
    }
}