using UnityEngine;

namespace OppositeGame._project.Scripts.Utilities
{
    public static class CameraExtension
    {
        public static bool IsPointInViewport(this Camera camera, Vector3 worldPoint)
        {
            if (camera == null) return false;
            var viewportPoint = camera.WorldToViewportPoint(worldPoint);
            Debug.Log($"viewportPoint: {viewportPoint}");
            return viewportPoint.x is >= 0 and <= 1 && viewportPoint.y is >= 0 and <= 1;
        }
    }
}