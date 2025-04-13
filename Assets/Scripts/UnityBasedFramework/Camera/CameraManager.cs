﻿#region FILE HEADER
// Filename: CameraManager.cs
// Author: Kalulas
// Create: 2022-11-06
// Description:
// Design ->
// GPP ->
#endregion

using System.Collections.Generic;
using Framework.Debug;
using Framework.DesignPattern;
using UnityEngine;

namespace UnityBasedFramework.Camera
{
    public class CameraManager : Singleton<CameraManager>
    {
        #region Fields

        private const int CAMERA_NUM_LIMIT = 10;
        private int m_LastAddCameraIdx;
        private UnityEngine.Camera[] m_CameraArray;

        #endregion

        #region Singleton

        private CameraManager()
        {
            
        }

        public override void OnSingletonInit()
        {
            m_LastAddCameraIdx = 0;
            m_CameraArray = new UnityEngine.Camera[CAMERA_NUM_LIMIT];
        }

        public override void OnSingletonDisposed()
        {
            m_CameraArray = null;
        }

        #endregion

        #region Public Interface

        public int RegisterCamera(UnityEngine.Camera camera)
        {
            if (camera == null)
            {
                Log.Error("[CameraManager.RegisterCamera] null camera received, exit!");
                return -1;
            }
            
            var cameraID = m_LastAddCameraIdx++;
            m_CameraArray[cameraID] = camera;
            return cameraID;
        }

        public UnityEngine.Camera GetCamera(int cameraID)
        {
            if (cameraID < 0 || cameraID >= m_CameraArray.Length)
            {
                return null;
            }

            return m_CameraArray[cameraID];
        }

        #endregion

    }
}