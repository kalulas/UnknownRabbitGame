#region FILE HEADER
// Filename: LocalPlayerInputProvider.cs
// Author: Kalulas
// Create: 2022-12-04
// Description:
#endregion

using Framework.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnknownRabbitGame.InputSystem
{
    [RequireComponent(typeof(PlayerInput))]
    public class LocalPlayerInputProvider : MonoBehaviour, IInputProvider
    {
        #region Fields

        private PlayerInput m_PlayerInputComp;
        private InputMessage[] m_InputMessages;

        #endregion

        #region Input Provider

        public InputMessage[] Provide()
        {
            return m_InputMessages;
        }

        #endregion

        #region Event Function

        private void Awake()
        {
            m_InputMessages = new[]
            {
                new InputMessage
                {
                    InputType = InputMessageTypeDefine.MOVE
                }
            };
        }

        private void Start()
        {
            var comp = GetComponent<PlayerInput>();
            if (comp != null)
            {
                m_PlayerInputComp = comp;
            }
            
            m_PlayerInputComp.onActionTriggered += PlayerInputCompOnActionTriggered;
            m_PlayerInputComp.onControlsChanged += PlayerInputCompOnControlsChanged;
            m_PlayerInputComp.onDeviceLost += PlayerInputCompOnDeviceLost;
            m_PlayerInputComp.onDeviceRegained += PlayerInputCompOnDeviceRegained;
        }
        
        #endregion
        
        private void PlayerInputCompOnDeviceRegained(PlayerInput obj)
        {
            // throw new System.NotImplementedException();
        }

        private void PlayerInputCompOnDeviceLost(PlayerInput obj)
        {
            // throw new System.NotImplementedException();
        }

        private void PlayerInputCompOnControlsChanged(PlayerInput obj)
        {
            
        }

        private void PlayerInputCompOnActionTriggered(InputAction.CallbackContext obj)
        {
            // Debug.Log(obj);
            if (obj.action.name == InputActionDefine.MOVE)
            {
                var direction = obj.ReadValue<Vector2>();
                m_InputMessages[0].InputValue0 = direction.x;
                m_InputMessages[0].InputValue1 = direction.y;
            }
        }
    }
}