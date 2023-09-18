using OppositeGame._project.Scripts.mechanics.Bullets;
using OppositeGame._project.Scripts.mechanics.weapons;
using OppositeGame._project.Scripts.Patterns;
using UnityEngine;

namespace OppositeGame._project.Scripts.ScriptablesObjects.Weapons
{
    [CreateAssetMenu(fileName = "SingleShotWeapon", menuName = "OppositeGame/Weapons/SingleShotWeapon", order = 1)]
    public class SingleShotWeapon : WeaponStrategy
    {
        public override void Fire(Transform startTransform, int layer)
        {
            if(startTransform == null) return;
            var bullet = GetBullet();
            
            if(bullet)
            {
                PrepareBullet(bullet, startTransform, layer);
            };  
            
        }
    }
}