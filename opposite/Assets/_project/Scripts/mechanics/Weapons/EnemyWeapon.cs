﻿using OppositeGame._project.Scripts.CameraScripts;
using OppositeGame._project.Scripts.Utilities;
using UnityEngine;

namespace OppositeGame._project.Scripts.mechanics.weapons
{
    public class EnemyWeapon : Weapon
    {
        [SerializeField] private Camera camera;
        private float _nextFireTime;
        private bool IsInViewPort => camera.IsPointInViewport(transform.position);
        
        private void Awake()
        {
            weaponStrategy.Initialize();
            camera ??= Camera.main;
        }
        
        private void Update()
        {
            if (!IsInViewPort || !(Time.time >= _nextFireTime)) return;
            
            weaponStrategy.Fire(startTransform, layer);
            _nextFireTime = Time.time + weaponStrategy.fireRate;
        }
    }
}