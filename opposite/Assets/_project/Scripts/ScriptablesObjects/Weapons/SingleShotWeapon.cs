using OppositeGame._project.Scripts.mechanics.Bullets;
using OppositeGame._project.Scripts.mechanics.Magnetism;
using OppositeGame._project.Scripts.mechanics.weapons;
using OppositeGame._project.Scripts.Patterns;
using UnityEngine;

namespace OppositeGame._project.Scripts.ScriptablesObjects.Weapons
{
    [CreateAssetMenu(fileName = "SingleShotWeapon", menuName = "OppositeGame/Weapons/SingleShotWeapon", order = 1)]
    public class SingleShotWeapon : WeaponStrategy
    {
        public override void Fire(Transform startTransform, int layer, PolarityType polarity = PolarityType.Blue )
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