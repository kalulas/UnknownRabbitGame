#region FILE HEADER
// Filename: GameSceneManager.cs
// Author: Kalulas
// Create: 2022-10-31
// Description:
// Design ->
// GPP ->
#endregion

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

        public void StartNewGame(BaseGame game)
        {
            // TODO exit previous game
            m_CurrentGame = game;
            m_CurrentGame.Init();
            m_CurrentGame.Start();
        }

        public void Start()
        {
            
        }

        public void Update()
        {
            m_CurrentGame?.Update();
        }

        public void OnDestroy()
        {
            
        }
    }
}