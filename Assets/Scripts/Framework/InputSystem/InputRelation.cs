#region FILE HEADER
// Filename: InputRelation.cs
// Author: Kalulas
// Create: 2022-12-04
// Description:
#endregion

using System.Collections.Generic;

namespace Framework.InputSystem
{
    public class InputRelation
    {
        private IInputProvider m_Provider;
        private readonly List<IInputReceiver> m_ReceiverList;

        public InputRelation(IInputProvider provider)
        {
            m_Provider = provider;
            m_ReceiverList = new List<IInputReceiver>();
        }

        public InputRelation(IInputReceiver receiver)
        {
            m_Provider = null;
            m_ReceiverList = new List<IInputReceiver>
            {
                receiver
            };
        }

        public bool SetProvider(IInputProvider provider)
        {
            if (m_Provider != null)
            {
                return false;
            }

            m_Provider = provider;
            return true;
        }

        public bool AddReceiver(IInputReceiver receiver)
        {
            m_ReceiverList.Add(receiver);
            return true;
        }

        public bool Process()
        {
            if (m_Provider == null || m_ReceiverList.Count == 0)
            {
                return false;
            }

            var messages = m_Provider.Provide();
            for (int i = 0; i < m_ReceiverList.Count; i++)
            {
                var receiver = m_ReceiverList[i];
                receiver.Receive(messages);
            }

            return true;
        }
    }
}