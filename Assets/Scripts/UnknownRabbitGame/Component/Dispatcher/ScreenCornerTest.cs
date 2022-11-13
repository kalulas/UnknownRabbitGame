#region FILE HEADER
// Filename: Test.cs
// Author: Kalulas
// Create: 2022-06-19
// Description:
// Design ->
// GPP ->
#endregion

using Framework.GameScene;
using UnityBasedFramework.Utils;
using UnityEngine;
using UnknownRabbitGame.EventSystem;

namespace UnknownRabbitGame.Component.Dispatcher
{
    public class ScreenCornerTest : MonoBehaviour
    {
        private Camera m_Camera;

        private void Start()
        {
            m_Camera = Camera.main;
        }

        private void Update()
        {
            if (m_Camera == null)
            {
                return;
            }

            var result = VisualAssist.GetScreenCorners(gameObject, m_Camera, true);
            // Debug.LogFormat("screen corners of {0} is {1}", gameObject.name, result);
            GameSceneManager.Instance.CurrentGame.GetEventDispatcher().DispatchEvent((uint)EventDefine.On3DObjectScreenBoundsUpdate, gameObject.name, result);
            // m_EventDispatcher.DispatchEvent((uint)EventDefine.On3DObjectScreenBoundsUpdate, result);
        }
    }
}