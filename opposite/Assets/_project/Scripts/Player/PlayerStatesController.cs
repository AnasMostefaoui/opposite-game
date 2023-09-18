using System;
using OppositeGame._project.Scripts.mechanics;
using OppositeGame._project.Scripts.mechanics.weapons;
using OppositeGame._project.Scripts.ScriptablesObjects.Weapons;
using UnityEngine;

namespace OppositeGame._project.Scripts.Player
{
    public class WeaponPowerUp
    {
        
    }
    
    [RequireComponent(typeof(Destructible))]
    public class PlayerStatesController  : MonoBehaviour
    {
        [SerializeField] private float lifePoints = 5f;
        [SerializeField] private WeaponStrategy currentWeapon;

        private PlayerWeapon _weaponHolder;
        private Destructible _destructible;
        private Camera _camera;
        private float _currentLifePoints;

        public float LifePoints
        {
            set => _destructible.LifePoints = value;
            get => _destructible.LifePoints;
        }

        private void Start()
        {
            _destructible ??= GetComponent<Destructible>();
            _destructible.LifePoints = lifePoints;
            _camera ??= Camera.main;
        }
        
        private void ChangeWeapon(WeaponStrategy weapon)
        {
            _weaponHolder.ChangeWeaponStrategy(currentWeapon);
        }
    }
}