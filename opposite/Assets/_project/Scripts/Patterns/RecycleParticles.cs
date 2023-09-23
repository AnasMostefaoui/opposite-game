using System;
using UnityEngine;

namespace OppositeGame._project.Scripts.Patterns
{
    public class RecycleParticles : MonoBehaviour
    {
        private void OnParticleSystemStopped()
        {
            ObjectPoolManager.Recycle(gameObject);
        }
    }
}