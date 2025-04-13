#region FILE HEADER
// Filename: TestInputReceiver.cs
// Author: Kalulas
// Create: 2022-12-04
// Description:
#endregion

using Framework.InputSystem;
using UnityBasedFramework.GameScene;
using UnityEngine;

namespace UnknownRabbitGame.InputSystem
{
    public class TestInputReceiver : MonoBehaviour, IInputReceiver
    {
        public void Receive(InputMessage[] messages)
        {
            var frameLength = GameSceneManager.Instance.CurrentGame.GetFrameLength();
            var direction = new Vector3(messages[0].InputValue0, 0, messages[0].InputValue1);
            gameObject.transform.Translate(direction * frameLength);
        }
    }
}