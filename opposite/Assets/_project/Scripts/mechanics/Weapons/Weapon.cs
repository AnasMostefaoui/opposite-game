using System.Collections.Generic;
using OppositeGame._project.Scripts.mechanics.Bullets;
using OppositeGame._project.Scripts.ScriptablesObjects.Weapons;
using UnityEngine;
using UnityEngine.Serialization;

namespace OppositeGame._project.Scripts.mechanics.weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected WeaponStrategy weaponStrategy;
        [FormerlySerializedAs("startPosition")] [SerializeField] protected Transform startTransform;
        [SerializeField] protected int layer;

        private IShootingPattern _shootingPattern; 
        
        private void OnValidate()
        {
            layer = gameObject.layer;
        }
        
        public void changeWeaponStrategy(WeaponStrategy newWeaponStrategy)
        {
            weaponStrategy = newWeaponStrategy;
            weaponStrategy.setupWeapon();
        }
    }


    public class ShootThreeBullets : IShootingPattern
    {
        public List<Bullet> Shoot()
        {
            var bullets = new List<Bullet>()
            {
                new Bullet(),
                new Bullet(),
                new Bullet()
            };
            
            
            return bullets;
        }
    }

    public interface IShootingPattern
    {
        List<Bullet> Shoot();
    }
    
}