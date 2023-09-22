using System;
using OppositeGame._project.Scripts.ScriptablesObjects.Pools;
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