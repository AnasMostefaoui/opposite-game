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
            if (!IsInViewPort || !(Time.time >= NextFireTime)) return;
            
            CurrentWeaponStrategy.Fire(startTransform, layer);
            NextFireTime = Time.time + CurrentWeaponStrategy.fireRate;
        }
    }
}