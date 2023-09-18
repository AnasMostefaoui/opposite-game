using UnityEngine;

namespace OppositeGame._project.Scripts.Utilities
{
    public static class CameraExtension
    {
        public static bool IsPointInViewport(this Camera camera, Vector3 worldPoint)
        {
            var viewportPoint = camera.WorldToViewportPoint(worldPoint);
            return viewportPoint.x is >= 0 and <= 1 && viewportPoint.y is >= 0 and <= 1;
        }
    }
}