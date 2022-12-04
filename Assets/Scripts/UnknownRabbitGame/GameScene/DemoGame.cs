#region FILE HEADER
// Filename: DemoGameScene.cs
// Author: Kalulas
// Create: 2022-10-31
// Description:
// Design ->
// GPP ->
#endregion

using Framework.GameScene;
using UnityBasedFramework.Camera;
using UnityBasedFramework.Entity;
using UnityBasedFramework.InputSystem;
using UnityBasedFramework.Resources;
using UnityEngine;
using UnknownRabbitGame.Component.Dispatcher;

namespace UnknownRabbitGame.GameScene
{
    public class DemoGame : BaseGame
    {
        #region Fields

        private const float m_FrameLength = 0.05f;

        private const string m_3DRootName = "3DRoot";
        private const string m_UIRootName = "UIRoot";
        private const string m_PreviewRootName = "PreviewRoot";
        
        private const string m_3DCameraName = "3DCamera";
        private const string m_UICameraName = "UICamera";
        private const string m_PreviewCameraName = "PreviewCamera";

        private int m_3DCameraID;
        private int m_UICameraID;
        private int m_PreviewCameraID;

        private GameObject m_GameSceneContainer;

        private Transform m_3DRoot;
        private Transform m_UIRoot;
        private Transform m_PreviewRoot;

        #endregion

        #region Properties

        /// <summary>
        /// 3D camera
        /// </summary>
        public int MainCameraID => m_3DCameraID;
        public int UICameraID => m_UICameraID;
        public int PreviewCameraID => m_PreviewCameraID;

        #endregion
        
        #region BaseGame

        protected override void OnGameStart()
        {
            LoadGameContainer();
        }

        protected override void OnGameSceneReady()
        {
            // custom logic ...
            LoadRabbitEntity();
        }
        
        protected override void OnGameUpdate(float deltaTime)
        {
            EntityManager.Instance.Update(deltaTime);
        }
        
        protected override void OnGameFrameUpdate(uint frameCount)
        {
            // Debug.Log($"[DemoGame.OnGameFrameUpdate] customFrameCount:{frameCount} / engineFrameCount:{Time.frameCount}");
            InputManager.Instance.FrameUpdate(m_FrameLength);
            EntityManager.Instance.FrameUpdate(m_FrameLength);
        }
        
        protected override void OnGameExit()
        {
            Debug.Log("[DemoGame.OnGameExit]");
        }

        public override float GetFrameLength()
        {
            return m_FrameLength;
        }

        #endregion

        #region Private

        private void LoadRabbitEntity()
        {
            EntityManager.Instance.CreateEntity("RabbitBrownWhite", m_3DRoot, AfterEntityCreated);
        }

        private void AfterEntityCreated(bool success, uint entityID, GameObject entity)
        {
            if (!success)
            {
                Debug.LogError("[DemoGame.AfterEntityCreated] created entity failed!");
                return;
            }
            
            Debug.Log($"[DemoGame.AfterEntityCreated] {entity.name} created!");
            var rabbit = EntityManager.Instance.GetEntity(entityID);
            if (rabbit == null)
            {
                return;
            }
            
            Debug.Log($"[DemoGame.AfterEntityCreated] entity({entityID}) position: {rabbit.GameObject.transform.localPosition}");
            rabbit.GameObject.AddComponent<ScreenCornerTest>();
        }

        private void TryLocateContainerRoot()
        {
            if (m_GameSceneContainer == null)
            {
                Debug.LogWarning("[DemoGame.TryLocateContainerRoot] container not found, exit!");
                return;
            }

            m_3DRoot = m_GameSceneContainer.transform.Find(m_3DRootName);
            m_UIRoot = m_GameSceneContainer.transform.Find(m_UIRootName);
            m_PreviewRoot = m_GameSceneContainer.transform.Find(m_PreviewRootName);

            if (m_3DRoot == null)
            {
                Debug.LogError($"[DemoGame.TryLocateContainerRoot] {m_3DRootName} not found!");
            }

            if (m_UIRoot == null)
            {
                Debug.LogError($"[DemoGame.TryLocateContainerRoot] {m_UIRootName} not found!");
            }

            if (m_PreviewRoot == null)
            {
                Debug.LogError($"[DemoGame.TryLocateContainerRoot] {m_PreviewRootName} not found!");
            }
        }

        private void TryFindCameraInContainer()
        {
            if (m_GameSceneContainer == null)
            {
                Debug.LogWarning("[DemoGame.TryFindCameraInContainer] container not found, exit!");
                return;
            }

            m_3DCameraID = CameraManager.Instance.RegisterCamera(m_GameSceneContainer.transform.Find(m_3DCameraName)
                ?.GetComponent<Camera>());
            m_UICameraID = CameraManager.Instance.RegisterCamera(m_GameSceneContainer.transform.Find(m_UICameraName)
                ?.GetComponent<Camera>());
            m_PreviewCameraID = CameraManager.Instance.RegisterCamera(m_GameSceneContainer.transform
                .Find(m_PreviewCameraName)?.GetComponent<Camera>());
        }

        private async void LoadGameContainer()
        {
            m_GameSceneContainer = await ResourceManager.Instance.LoadAndInstantiateAsync<GameObject>("BaseGameContainer");
            TryLocateContainerRoot();
            TryFindCameraInContainer();
            OnGameSceneReady();
        }
        
        #endregion
    }
}