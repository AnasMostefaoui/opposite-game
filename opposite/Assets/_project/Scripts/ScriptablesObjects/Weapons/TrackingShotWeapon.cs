using OppositeGame._project.Scripts.mechanics.Bullets;
using OppositeGame._project.Scripts.mechanics.Magnetism;
using OppositeGame._project.Scripts.Utilities;
using UnityEngine;

namespace OppositeGame._project.Scripts.ScriptablesObjects.Weapons
{
    [CreateAssetMenu(fileName = "TrackingShotWeapon", menuName = "OppositeGame/Weapons/TrackingShotWeapon", order = 1)]
    public class TrackingShotWeapon : WeaponStrategy
    {
        [SerializeField] public float trackingSpeed = 5f;
        private Transform _target;
        public override void Initialize()
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        public override void Fire(Transform startTransform, int layer, PolarityType polarity = PolarityType.Blue)
        {
            if(startTransform == null) return;
            var bullet = GetBullet();
            if(bullet)
            {
                PrepareBullet(bullet, startTransform, layer);
                bullet.OnUpdate = () =>
                {
                    var direction = (_target.position - bullet.transform.position).With(z: 0).normalized;
                    bullet.transform.LookAt2D(direction, true, trackingSpeed);
                };
            };  
           
        }
    }
}