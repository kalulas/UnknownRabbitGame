#region FILE HEADER
// Filename: TestInputReceiver.cs
// Author: Kalulas
// Create: 2022-12-04
// Description:
#endregion

using Framework.InputSystem;
using UnityEngine;

namespace UnityBasedFramework.InputSystem
{
    public class TestInputReceiver : MonoBehaviour, IInputReceiver
    {
        private void Start()
        {
            InputManager.Instance.RegisterReceiver("MainInput", this);
        }

        public void Receive(InputMessage[] messages)
        {
            Debug.Log($"[TestInputReceiver.Receive] {messages[0].InputType} -> {messages[0].InputValue0}:{messages[0].InputValue1}");
        }
    }
}