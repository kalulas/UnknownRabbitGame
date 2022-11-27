#region FILE HEADER
// Filename: ScreenCornerController.cs
// Author: Kalulas
// Create: 2022-06-19
// Description:
// Design ->
// GPP ->
#endregion

using System;
using System.Collections;
using UnknownRabbitGame.EventSystem;
using TMPro;
using UnityBasedFramework.GameScene;
using UnityEngine;
using UnityEngine.UI;

namespace UnknownRabbitGame.Component.UI
{
    public class BoundsSelectController : MonoBehaviour
    {
        public Image selectorImage;
        public TextMeshProUGUI selectorText;

        private IEnumerator Start()
        {
            yield return null;
            GameSceneManager.Instance.CurrentGame.GetEventDispatcher().RegisterEvent((uint)EventDefine.On3DObjectScreenBoundsUpdate, OnTargetObjectScreenBoundsUpdate);
        }

        private void OnDestroy()
        {
            GameSceneManager.Instance.CurrentGame.GetEventDispatcher().UnregisterEvent((uint)EventDefine.On3DObjectScreenBoundsUpdate, OnTargetObjectScreenBoundsUpdate);
        }

        private void OnTargetObjectScreenBoundsUpdate(object[] param)
        {
            if (param.Length != 2)
            {
                return;
            }

            var targetName = (string) param[0];
            var vec4 = (Vector4) param[1];
            // Debug.LogFormat("[ScreenCornerController.OnTargetObjectScreenBoundsUpdate] {0}", vec4);
            var centerX = (vec4[2] + vec4[0]) / 2;
            var centerY = (vec4[3] + vec4[1]) / 2;
            var width = vec4[2] - vec4[0];
            var height = vec4[3] - vec4[1];
            
            
            selectorImage.rectTransform.anchoredPosition = new Vector2(centerX, centerY);
            selectorImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            selectorImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);

            selectorText.text = $"{targetName}\nminX:{vec4[0]} minY:{vec4[1]}\nmaxX:{vec4[2]} maxY:{vec4[3]}";
            selectorImage.gameObject.SetActive(width > 0.001f && height > 0.001f);
        }
    }
}