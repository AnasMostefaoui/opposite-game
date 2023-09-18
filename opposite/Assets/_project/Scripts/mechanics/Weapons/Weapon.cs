using System;
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
        
        protected float NextFireTime;
        protected Camera CameraObject;
        private IShootingPattern _shootingPattern; 
        
        private void OnValidate()
        {
            layer = gameObject.layer;
        }

        private void OnEnable()
        {
            weaponStrategy.Initialize();
            CameraObject = Camera.main;
        }

        public void ChangeWeaponStrategy(WeaponStrategy newWeaponStrategy)
        {
            weaponStrategy = newWeaponStrategy;
            weaponStrategy.Initialize();
            weaponStrategy.SetupWeapon();
        }
    }

    

    public interface IShootingPattern
    {
        List<Bullet> Shoot();
    }
    
}