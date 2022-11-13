#region FILE HEADER
// Filename: CameraManager.cs
// Author: Kalulas
// Create: 2022-11-06
// Description:
// Design ->
// GPP ->
#endregion

using System.Collections.Generic;
using Framework.DesignPattern;
using UnityEngine;

namespace UnityBasedFramework.Camera
{
    public class CameraManager : Singleton<CameraManager>
    {
        #region Fields
        
        private List<UnityEngine.Camera> m_CameraList;

        #endregion

        #region Singleton

        private CameraManager()
        {
            Debug.Log("[CameraManager.CameraManager] ctor");
        }

        public override void OnSingletonInit()
        {
            m_CameraList = new List<UnityEngine.Camera>();
        }

        public override void OnSingletonDisposed()
        {
            m_CameraList.Clear();
            m_CameraList = null;
        }

        #endregion

        #region Public Interface

        public int RegisterCamera(UnityEngine.Camera camera)
        {
            if (camera == null)
            {
                Debug.LogError("[CameraManager.RegisterCamera] null camera received, exit!");
                return -1;
            }
            
            var cameraID = m_CameraList.Count;
            m_CameraList.Add(camera);
            return cameraID;
        }

        #endregion

    }
}