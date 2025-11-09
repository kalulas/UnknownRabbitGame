#region FILE HEADER
// Filename: UIPreviewManager.cs
// Author: Kalulas
// Create: 2022-10-31
// Description:
// Design ->
// GPP ->
#endregion

using Framework.DesignPattern;
using Framework.Logging;
using UnityEngine;
using UnityEngine.UI;

namespace UnknownRabbitGame.Manager
{
    public class UIPreviewManager : Singleton<UIPreviewManager>
    {
        private RenderTexture m_RenderTex;
        private Camera m_PreviewCam;
        
        #region Singleton

        private UIPreviewManager()
        {
            
        }
        
        public override void OnSingletonInit()
        {
            TryFindPreviewCamera();
        }

        public override void OnSingletonDisposed()
        {
            
        }

        #endregion

        #region Private Utils

        private void TryFindPreviewCamera()
        {
            foreach (var cam in Camera.allCameras)
            {
                if (cam.CompareTag("PreviewCamera"))
                {
                    m_PreviewCam = cam;
                    break;
                }
            }
        }

        #endregion

        #region Public Interface

        /// <summary>
        /// create preview with RenderTexture(rawImage.sizeDelta.x, rawImage.sizeDelta.y)
        /// </summary>
        /// <param name="rawImage">enable set to false, if CreatePreview failed; else set to true</param>
        public void CreatePreview(RawImage rawImage)
        {
            if (m_PreviewCam == null)
            {
                rawImage.enabled = false;
                Log.Warning("[UIPreviewManager.CreatePreview] previewCam not found, exit");
                return;
            }
            
            var sizeDelta = rawImage.rectTransform.sizeDelta;
            var width = (int)sizeDelta.x;
            var height = (int)sizeDelta.y;
            var depth = 0;
            m_RenderTex = new RenderTexture(width, height, depth);
            m_RenderTex.name = $"RT_width{width}_height{height}_depth{depth}";
            m_PreviewCam.targetTexture = m_RenderTex;
            rawImage.texture = m_RenderTex;
            rawImage.enabled = true;
        }

        #endregion
    }
}