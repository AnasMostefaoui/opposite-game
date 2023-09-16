using System;
using OppositeGame._project.Scripts.Inputs;
using UnityEngine;

namespace OppositeGame._project.Scripts.mechanics.weapons
{
    public class PlayerWeapon : Weapon
    {
        private InputReader _inputReader;
        private float _nextFireTime;

        private void Awake()
        {
            _inputReader = GetComponent<InputReader>();
        }

        private void Update()
        {
            if (!_inputReader.IsFiring || !(Time.time >= _nextFireTime)) return;
            
            weaponStrategy.Fire(startTransform, layer);
            _nextFireTime = Time.time + weaponStrategy.fireRate;
        }
    }
}