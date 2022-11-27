#region FILE HEADER
// Filename: InputReceiver.cs
// Author: Kalulas
// Create: 2022-11-27
// Description: Receiver of PlayerInput component
#endregion

using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityBasedFramework.InputSystem
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputReceiver : MonoBehaviour
    {
        #region Fields

        private PlayerInput m_PlayerInputComp;

        #endregion
        
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
            // TODO starts with Player/Move
            // cache all Move action in one logic frame time in InputManager(maybe), and update position in GameFrameUpdate(fixed, 100ms?)
        }
    }
}