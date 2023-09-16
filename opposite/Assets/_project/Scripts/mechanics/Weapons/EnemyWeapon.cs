using UnityEngine;

namespace OppositeGame._project.Scripts.mechanics.weapons
{
    public class EnemyWeapon : Weapon
    {
        private float _nextFireTime;
        
        private void Update()
        {
            if (!(Time.time >= _nextFireTime)) return;
            
            weaponStrategy.Fire(startTransform, layerMask);
            _nextFireTime = Time.time + weaponStrategy.fireRate;
        }
    }
}