using OppositeGame._project.Scripts.CameraScripts;
using OppositeGame._project.Scripts.Utilities;
using UnityEngine;

namespace OppositeGame._project.Scripts.mechanics.weapons
{
    public class EnemyWeapon : Weapon
    { 
        private bool IsInViewPort => CameraObject.IsPointInViewport(transform.position);
        
        private void Update()
        {
            //FireRateCounter += Time.deltaTime;
            
            if (!IsInViewPort || !DidReload) return;
            
            CurrentWeaponStrategy.Fire(startTransform, layer);
           // FireRateCounter = 0;
        }
    }
}