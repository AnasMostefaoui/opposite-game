﻿using System;
using OppositeGame._project.Scripts.Inputs;
using OppositeGame._project.Scripts.mechanics.Collectibles;
using OppositeGame._project.Scripts.ScriptablesObjects.Pools;
using OppositeGame._project.Scripts.ScriptablesObjects.Weapons;
using Unity.VisualScripting;
using UnityEngine;

namespace OppositeGame._project.Scripts.mechanics.weapons
{
    public class PlayerWeapon : Weapon
    {
        private InputReader _inputReader; 
        private void Awake()
        {
            _inputReader = GetComponent<InputReader>();
        }

        private void OnDisable()
        {
            ChangeWeaponStrategy(weaponStrategy);
        }

        private void Update()
        {
            if (!_inputReader.IsFiring || !(Time.time >= NextFireTime)) return;
            
            CurrentWeaponStrategy.Fire(startTransform, layer);
            NextFireTime = Time.time + CurrentWeaponStrategy.fireRate;
        }
        
        // if you take power up change the weapon based on the powerup content
        private void OnTriggerEnter2D(Collider2D other)
        {
            var powerUp = other.GetComponent<WeaponCollectible>();
            if (powerUp == null) return;
            ChangeWeaponStrategy(powerUp.weaponStrategy);
            Destroy(other.gameObject);
        }
    }
}