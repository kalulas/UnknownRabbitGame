#region FILE HEADER
// Filename: Test.cs
// Author: Kalulas
// Create: 2022-06-19
// Description:
// Design ->
// GPP ->
#endregion

using UnityBasedFramework.Camera;
using UnityBasedFramework.GameScene;
using UnityBasedFramework.Utils;
using UnityEngine;
using UnknownRabbitGame.EventSystem;
using UnknownRabbitGame.GameScene;

namespace UnknownRabbitGame.Component.Dispatcher
{
    public class ScreenCornerTest : MonoBehaviour
    {
        private Camera m_Camera;

        private void Start()
        {
            if (GameSceneManager.Instance.CurrentGame is not DemoGame demoGame)
            {
                return;
            }
            
            var mainCameraID = demoGame.MainCameraID;
            var targetCam = CameraManager.Instance.GetCamera(mainCameraID);
            m_Camera = targetCam;
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