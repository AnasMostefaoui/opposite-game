using System;
using OppositeGame._project.Scripts.Inputs;
using OppositeGame._project.Scripts.ScriptablesObjects.Pools;
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
        
        private void Update()
        {
            if (!_inputReader.IsFiring || !(Time.time >= NextFireTime)) return;
            
            weaponStrategy.Fire(startTransform, layer);
            NextFireTime = Time.time + weaponStrategy.fireRate;
        }
    }
}