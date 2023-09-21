using System;
using System.Collections.Generic;
using OppositeGame._project.Scripts.mechanics.Bullets;
using OppositeGame._project.Scripts.ScriptablesObjects.Pools;
using OppositeGame._project.Scripts.ScriptablesObjects.Weapons;
using UnityEngine;
using UnityEngine.Serialization;

namespace OppositeGame._project.Scripts.mechanics.weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected WeaponStrategy weaponStrategy;
        [SerializeField] protected BulletsPool bulletsPool;
        [FormerlySerializedAs("startPosition")] [SerializeField] protected Transform startTransform;
        [SerializeField] protected int layer;
        
        protected WeaponStrategy CurrentWeaponStrategy;
        protected float FireRateCounter;
        protected Camera CameraObject;
        private IShootingPattern _shootingPattern;
        protected bool DidReload => FireRateCounter >= CurrentWeaponStrategy.fireRate;
        private void OnValidate()
        {
            layer = gameObject.layer;
        }

        private void OnEnable()
        {
            CurrentWeaponStrategy = weaponStrategy;
            CurrentWeaponStrategy.bulletsPool = bulletsPool;
            CurrentWeaponStrategy.Initialize();
            CameraObject = Camera.main;
        }

        public void ChangeWeaponStrategy(WeaponStrategy newWeaponStrategy)
        {
            CurrentWeaponStrategy = newWeaponStrategy;
            CurrentWeaponStrategy.Initialize();
            CurrentWeaponStrategy.SetupWeapon();
        }
    }

    

    public interface IShootingPattern
    {
        List<Bullet> Shoot();
    }
    
}