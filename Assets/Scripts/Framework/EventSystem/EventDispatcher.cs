#region FILE HEADER
// Filename: EventDispatcher.cs
// Author: Kalulas
// Create: 2022-06-19
// Description:
// Design ->
// GPP ->
#endregion

using System.Collections.Generic;

namespace Framework.EventSystem
{
    public interface IEventDispatcher
    {
        public EventDispatcher GetEventDispatcher();
    }
    
    public class EventDispatcher
    {
        private readonly Dictionary<uint, EventHandler> m_RegisteredHandler = new Dictionary<uint, EventHandler>();

        public void RegisterEvent(uint eventID, EventHandler handler)
        {
            if (m_RegisteredHandler.ContainsKey(eventID))
            {
                m_RegisteredHandler[eventID] += handler;
            }
            else
            {
                m_RegisteredHandler.Add(eventID, handler);
            }
        }

        public void UnregisterEvent(uint eventID, EventHandler handler)
        {
            if (!m_RegisteredHandler.ContainsKey(eventID))
            {
                return;
            }
            
            m_RegisteredHandler[eventID] -= handler;
            var remains = m_RegisteredHandler[eventID];
            if (remains == null)
            {
                m_RegisteredHandler.Remove(eventID);
            }
        }

        public void DispatchEvent(uint eventID, params object[] param)
        {
            if (!m_RegisteredHandler.ContainsKey(eventID))
            {
                return;
            }

            var handler = m_RegisteredHandler[eventID];
            handler.Invoke(param);
        }
    }
}