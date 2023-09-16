using UnityEngine;

namespace OppositeGame._project.Scripts.mechanics.Bullets
{
    public class ProjectileDestroyer : MonoBehaviour
    {
        // This is called by the animation event at the end of the animation
        // it allow us to hook into the end of animation to destroy objects.
        void OnAnimationDone()
        {
            Destroy(gameObject);
        }
    }
}
