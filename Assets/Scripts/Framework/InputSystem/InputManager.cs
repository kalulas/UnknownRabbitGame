#region FILE HEADER
// Filename: InputManager.cs
// Author: Kalulas
// Create: 2022-12-04
// Description:
#endregion

using System.Collections.Generic;
using Framework.DesignPattern;

namespace Framework.InputSystem
{
    public class InputManager : Singleton<InputManager>
    {
        public const uint INPUT_IDENTIFIER_MAX = 256;
        
        private uint m_CurrentInputIdentifier;
        private InputRelation[] m_InputRelations; 
        private Dictionary<string, uint> m_LabelToIdentifierDict;

        #region Singleton

        private InputManager()
        {
            
        }

        public override void OnSingletonInit()
        {
            m_LabelToIdentifierDict = new Dictionary<string, uint>();
            m_InputRelations = new InputRelation[INPUT_IDENTIFIER_MAX];
        }

        public override void OnSingletonDisposed()
        {
            m_LabelToIdentifierDict.Clear();
            m_LabelToIdentifierDict = null;
        }

        #endregion

        #region Event Function

        public void FrameUpdate(float frameLength)
        {
            for (int i = 0; i < m_CurrentInputIdentifier; i++)
            {
                m_InputRelations[i].Process();
            }
        }

        #endregion

        #region Private Util

        private uint GenerateIdentifierWithLabel(string inputLabel)
        {
            if (m_CurrentInputIdentifier + 1 == INPUT_IDENTIFIER_MAX)
            {
                return INPUT_IDENTIFIER_MAX;
            }

            if (m_LabelToIdentifierDict.TryGetValue(inputLabel, out var identifier))
            {
                return identifier;
            }

            identifier = m_CurrentInputIdentifier;
            m_CurrentInputIdentifier++;
            m_LabelToIdentifierDict.Add(inputLabel, identifier);
            return identifier;
        }

        #endregion

        #region Public Interface
        
        public void RegisterProvider(string inputLabel, IInputProvider provider)
        {
            var identifier = GenerateIdentifierWithLabel(inputLabel);
            if (identifier == INPUT_IDENTIFIER_MAX)
            {
                return;
            }

            if (m_InputRelations[identifier] == null)
            {
                m_InputRelations[identifier] = new InputRelation(provider);
            }
            else
            {
                m_InputRelations[identifier].SetProvider(provider);
            }
        }

        public void RegisterReceiver(string inputLabel, IInputReceiver receiver)
        {
            var identifier = GenerateIdentifierWithLabel(inputLabel);
            if (identifier == INPUT_IDENTIFIER_MAX)
            {
                return;
            }
            
            if (m_InputRelations[identifier] == null)
            {
                m_InputRelations[identifier] = new InputRelation(receiver);
            }
            else
            {
                m_InputRelations[identifier].AddReceiver(receiver);
            }
        }

        #endregion

    }
}