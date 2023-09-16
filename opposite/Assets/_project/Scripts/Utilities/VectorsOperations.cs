using UnityEngine;

namespace OppositeGame._project.Scripts.Utilities
{
   
    public static class TransformExtensions
    {
        /**
         * LookAt2D is a helper method to rotate a transform to look at a target in 2D
         * @param transform the transform to rotate
         * @param target the target to look at
         */
        public static void LookAt2D(this Transform transform, Vector2 target)
        {
            // update the direction of the sprite based on the velocity vector
            var angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}