#region FILE HEADER
// Filename: ScreenCornersGenerate.cs
// Author: Kalulas
// Create: 2022-06-19
// Description:
// Design ->
// GPP ->
#endregion

using UnityEngine;

namespace UnknownRabbitGame.Helpers
{
    public static class VisualAssist
    {
        /// <summary>
        /// calculate all worldCorners with bounds, and translate into screen point(minX, minY, maxX, maxY)
        /// </summary>
        /// <param name="target"></param>
        /// <param name="camera"></param>
        /// <param name="zTest">if ture, x and y will be set to 0 if z less or equal than 0 if screen point</param>
        /// <returns></returns>
        public static Vector4 GetScreenCorners(GameObject target, UnityEngine.Camera camera, bool zTest)
        {
            var renderer = target.GetComponentInChildren<Renderer>();
            if (renderer == null)
            {
                return Vector4.zero;
            }

            var bounds = renderer.bounds;
            var center = bounds.center;
            var extents = bounds.extents;

            var worldCorners = new[]
            {
                new Vector3(center.x - extents.x, center.y - extents.y, center.z - extents.z),
                new Vector3(center.x - extents.x, center.y - extents.y, center.z + extents.z),
                new Vector3(center.x - extents.x, center.y + extents.y, center.z - extents.z),
                new Vector3(center.x - extents.x, center.y + extents.y, center.z + extents.z),
                new Vector3(center.x + extents.x, center.y - extents.y, center.z - extents.z),
                new Vector3(center.x + extents.x, center.y - extents.y, center.z + extents.z),
                new Vector3(center.x + extents.x, center.y + extents.y, center.z - extents.z),
                new Vector3(center.x + extents.x, center.y + extents.y, center.z + extents.z),
            };

            var screenCornerArray = new Vector3[8];
            for (int i = 0; i < 8; i++)
            {
                screenCornerArray[i] = camera.WorldToScreenPoint(worldCorners[i]);
                if (zTest && screenCornerArray[i].z <= 0)
                {
                    screenCornerArray[i].x = 0;
                    screenCornerArray[i].y = 0;
                }
            }
            
            var minX = float.MaxValue;
            var minY = float.MaxValue;
            var maxX = float.MinValue;
            var maxY = float.MinValue;
            for (int i = 0; i < screenCornerArray.Length; i++)
            {
                var corner = screenCornerArray[i];
                if (corner.x < minX)
                {
                    minX = corner.x;
                }

                if (corner.x > maxX)
                {
                    maxX = corner.x;
                }

                if (corner.y < minY)
                {
                    minY = corner.y;
                }

                if (corner.y > maxY)
                {
                    maxY = corner.y;
                }
            }
            
            return new Vector4(minX, minY, maxX, maxY);
        }
    }
}