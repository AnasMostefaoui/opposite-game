using UnityEngine;

namespace OppositeGame._project.Scripts.Utilities
{
   
    public static class TransformExtensions
    {
        /**
         * LookAt2D is a helper method to rotate a transform to look at a target in 2D
         * @param transform the transform to rotate
         * @param target the target to look at
         * @param shouldLerp should the rotation be smooth or instant
         * @param lerpSpeed the speed of the rotation if shouldLerp is true
         */
        public static void LookAt2D(this Transform transform, Vector2 target, bool shouldLerp = false, float lerpSpeed = 5f)
        {
            // update the direction of the sprite based on the velocity vector
            var angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg - 90f;
            var direction = target.normalized;
            // Okay, this one messed up with my mind quite a lot
            // In 2D, we want all rotation that occur in 2D plane to be around the Z axis
            // this is why we are setting Vector3.forward as the axis to rotate around (first param)
            // now the other vector is the direction we want to look at.
            var rotation = Quaternion.LookRotation(Vector3.forward, direction);
            if(shouldLerp)
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * lerpSpeed);
            else
                transform.rotation = rotation;
        }
    }

    public static class VectorExtensions
    {
        /**
         * With is a helper method to create a new Vector3 based on an existing one
         * @param original the original vector
         * @param x the new x value
         * @param y the new y value
         * @param z the new z value
         * @return a new Vector3 with the new values
         */
        public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
        }
    }
}