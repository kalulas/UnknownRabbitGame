#region FILE HEADER
// Filename: InputManager.cs
// Author: Kalulas
// Create: 2022-12-04
// Description:
#endregion

using Framework.DesignPattern;
using UnityEngine;

namespace UnityBasedFramework.InputSystem
{
    public class InputManager : Singleton<InputManager>
    {

        #region Singleton

        private InputManager()
        {
            Debug.Log("[InputManager.InputManager] ctor");
        }

        public override void OnSingletonInit()
        {
            // throw new System.NotImplementedException();
        }

        public override void OnSingletonDisposed()
        {
            // throw new System.NotImplementedException();
        }

        #endregion

        #region Event Function

        public void FrameUpdate(float frameLength)
        {
            
        }

        #endregion

        #region Public Interface
        
        // TODO register with entityID?
        
        public void RegisterProvider()
        {
            
        }

        public void RegisterReceiver()
        {
            
        }

        #endregion

    }
}