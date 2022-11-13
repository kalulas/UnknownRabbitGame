#region FILE HEADER
// Filename: GameSceneManager.cs
// Author: Kalulas
// Create: 2022-10-31
// Description:
// Design ->
// GPP ->
#endregion

using System;
using Framework.DesignPattern;

namespace Framework.GameScene
{
    public class GameSceneManager : Singleton<GameSceneManager>
    {
        private BaseGame m_CurrentGame;

        #region Properties

        public BaseGame CurrentGame => m_CurrentGame;

        #endregion

        #region Singleton
        
        private GameSceneManager()
        {
            
        }

        public override void OnSingletonInit()
        {
            
        }

        public override void OnSingletonDisposed()
        {
            
        }

        #endregion

        #region Event Function

        // public void OnCallerStart()
        // {
        //     
        // }

        public void OnCallerUpdate()
        {
            m_CurrentGame?.Update();
        }

        // public void OnCallerDestroy()
        // {
        //     
        // }

        #endregion

        #region Private Utils

        private void ExitPreviousGame()
        {
            if (m_CurrentGame != null)
            {
                m_CurrentGame.Exit();
                m_CurrentGame = null;
            }
        }

        #endregion

        #region Public Interface

        public void StartNewGame<TGame>() where TGame : BaseGame
        {
            ExitPreviousGame();
            m_CurrentGame = Activator.CreateInstance<TGame>();
            m_CurrentGame.Init();
            m_CurrentGame.Start();
        }

        public void StartNewGameWithScene<TGame>(string sceneIdentifier) where TGame : BaseGame
        {
            // TODO need unity based scene transition, move GameSceneManager to UnityBasedFramework, or abstract?
        }

        #endregion
    }
}