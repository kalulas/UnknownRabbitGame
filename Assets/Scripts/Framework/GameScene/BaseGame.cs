#region FILE HEADER
// Filename: BaseGame.cs
// Author: Kalulas
// Create: 2022-06-19
// Description:
// Design ->
// GPP ->
#endregion

using Framework.EventSystem;

namespace Framework.GameScene
{
    public abstract class BaseGame : IEventDispatcher
    {
        private EventDispatcher m_EventDispatcher;
        
        public void Init()
        {
            m_EventDispatcher = new EventDispatcher();
        }

        public EventDispatcher GetEventDispatcher()
        {
            return m_EventDispatcher;
        }
        
        protected abstract void OnGameUpdate(float deltaTime);
        protected abstract void OnGameFrameUpdate(uint frameCount);
        protected abstract void OnGameStart();
        protected abstract void OnGameSceneReady();
        protected abstract void OnGameExit();
        public abstract float GetFrameLength();

        public void Exit()
        {
            // set null OnGameExit
            OnGameExit();
            m_EventDispatcher = null;
        }

        public void Start()
        {
            OnGameStart();
        }

        public void Update(float deltaTime)
        {
            OnGameUpdate(deltaTime);
        }

        public void FrameUpdate(uint frameCount)
        {
            OnGameFrameUpdate(frameCount);
        }
    }
}