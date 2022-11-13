#region FILE HEADER
// Filename: PreviewTextureBinder.cs
// Author: Kalulas
// Create: 2022-10-31
// Description:
// Design ->
// GPP ->
#endregion

using UnityEngine;
using UnityEngine.UI;
using UnknownRabbitGame.Manager;

namespace UnknownRabbitGame.Component.UI
{
    public class PreviewTextureBinder : MonoBehaviour
    {
        public RawImage rawImage;
        private RenderTexture m_RenderTex;

        public void Start()
        {
            if (rawImage == null)
            {
                rawImage = GetComponent<RawImage>();
            }

            if (rawImage == null)
            {
                return;
            }

            UIPreviewManager.Instance.CreatePreview(rawImage);
        }
    }
}