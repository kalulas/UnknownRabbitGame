#region FILE HEADER
// Filename: GameFrameTicker.cs
// Author: Kalulas
// Create: 2022-12-04
// Description:
#endregion

namespace Framework.GameScene
{
    public delegate void GameFrameUpdate(uint frameCount);
    
    public class GameFrameTicker
    {
        private uint m_FrameCount;
        /// <summary>
        /// seconds
        /// </summary>
        private float m_Duration;
        /// <summary>
        /// seconds
        /// </summary>
        private readonly float m_FrameLength;
        private readonly GameFrameUpdate m_GameFrameUpdate;

        public uint FrameCount => m_FrameCount;
        public float FrameLength => m_FrameLength; 
        
        private GameFrameTicker(float frameLength, GameFrameUpdate frameUpdate)
        {
            m_FrameCount = 0;
            m_Duration = 0f;
            m_FrameLength = frameLength;
            m_GameFrameUpdate = frameUpdate;
        }

        #region Public Interface

        /// <summary>
        /// Create a GameFrameTicker with frame interval(seconds)
        /// </summary>
        /// <param name="frameLength"></param>
        /// <param name="frameUpdate"></param>
        /// <returns></returns>
        public static GameFrameTicker Create(float frameLength, GameFrameUpdate frameUpdate)
        {
            if (frameUpdate == null)
            {
                // TODO error message
                return null;
            }
            
            return new GameFrameTicker(frameLength, frameUpdate);
        }

        public void Tick(float delta)
        {
            m_Duration += delta;
            while (m_Duration >= m_FrameLength)
            {
                m_GameFrameUpdate(m_FrameCount);
                m_Duration -= m_FrameLength;
                m_FrameCount++;
            }
        }

        #endregion
    }
}