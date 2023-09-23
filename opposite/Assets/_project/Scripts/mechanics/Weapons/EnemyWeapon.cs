using OppositeGame._project.Scripts.CameraScripts;
using OppositeGame._project.Scripts.mechanics.Magnetism;
using OppositeGame._project.Scripts.Utilities;
using UnityEngine;

namespace OppositeGame._project.Scripts.mechanics.weapons
{
    public class EnemyWeapon : Weapon
    {
        [SerializeField] private PolarityType PolarityType;
        private bool IsInViewPort => CameraObject.IsPointInViewport(transform.position);
        
        private void Update()
        {
            FireRateCounter += Time.deltaTime;
            if (!IsInViewPort || !DidReload) return;
            CurrentWeaponStrategy.Fire(startTransform, layer, PolarityType);
           FireRateCounter = 0;
        }
    }
}