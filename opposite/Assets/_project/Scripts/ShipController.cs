using System;
using OppositeGame._project.Scripts.mechanics;
using UnityEngine;

namespace OppositeGame._project.Scripts
{
    public class ShipController: MonoBehaviour
    {
        [SerializeField] public Transform[] weaponMounts;
        [SerializeField] public float speed = 5f;
        [SerializeField] public float rotationSpeed = 5f;
        [SerializeField] public float maxSpeed = 5f;
        
        public float LifePoints
        {
            set => _currentLifePoints = value;
            get => _currentLifePoints;
        }
        private float _currentSpeed;
        private float _currentLifePoints;
        private Camera _camera;
        private Destructible _destructible;
        
        private void Start()
        {
            _camera ??= Camera.main;
            _destructible ??= GetComponent<Destructible>();
        }
    }
}