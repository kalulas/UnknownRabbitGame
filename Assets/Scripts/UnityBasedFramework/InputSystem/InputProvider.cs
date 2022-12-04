#region FILE HEADER
// Filename: InputProvider.cs
// Author: Kalulas
// Create: 2022-11-27
// Description: Receiver of PlayerInput component, Provider of InputManager
#endregion

using Framework.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityBasedFramework.InputSystem
{
    // TODO dynamic loading
    [RequireComponent(typeof(PlayerInput))]
    public class InputProvider : MonoBehaviour, IInputProvider
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
            m_InputMessages = new InputMessage[1];
            m_InputMessages[0] = new InputMessage
            {
                InputType = 0,
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
            // TODO dynamic register after load
            InputManager.Instance.RegisterProvider("MainInput", this);
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
            // cache all Move action in one logic frame time in InputManager(maybe), and update position in GameFrameUpdate(fixed, 50ms? -> 20frames / sec)
            if (obj.action.name == "Move")
            {
                var direction = obj.ReadValue<Vector2>();
                m_InputMessages[0].InputValue0 = direction.x;
                m_InputMessages[0].InputValue1 = direction.y;
            }
        }
    }
}