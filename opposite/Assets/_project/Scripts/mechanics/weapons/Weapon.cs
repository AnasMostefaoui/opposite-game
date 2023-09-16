using System.Collections.Generic;
using UnityEngine;

namespace OppositeGame._project.Scripts.mechanics.weapons
{
    public class Weapon : MonoBehaviour
    {
        IShootingPattern _shootingPattern;
        [SerializeField] private BulletType bulletType;
        
        void Shoot()
        {
            var bullets = _shootingPattern.Shoot();
            foreach (var bullet in bullets)
            {
                Instantiate(bullet, transform.position, Quaternion.identity);
            }
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