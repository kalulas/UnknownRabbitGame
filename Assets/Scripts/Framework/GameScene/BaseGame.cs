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

        public abstract void Start();

        public EventDispatcher GetEventDispatcher()
        {
            return m_EventDispatcher;
        }

        public abstract void OnGameSceneReady();

        public abstract void Update();

        public virtual void OnDestroy()
        {
            // set null OnGameExit
            // m_EventDispatcher = null;
        }
    }
}