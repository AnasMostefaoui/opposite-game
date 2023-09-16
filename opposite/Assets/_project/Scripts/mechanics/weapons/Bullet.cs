using UnityEngine;

namespace OppositeGame._project.Scripts.mechanics.weapons
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private BulletType bulletType;
        private Rigidbody2D _rb;
        private GameObject _firingEffect;
        private GameObject _hitEffect;
        private GameObject _bullet;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            if (_rb)
            {
                _rb.velocity = transform.up * bulletType.getSpeed();
                _rb.gravityScale = 0;
            }

            if (bulletType.firingEffectPrefab)
            {
                _firingEffect = Instantiate(bulletType.firingEffectPrefab, transform.position, Quaternion.identity);
                _firingEffect.transform.forward = gameObject.transform.forward;
                _firingEffect.transform.SetParent(transform);
                
                Destroy(_firingEffect, bulletType.lifeTime);
            }
        }

        private void Update()
        {
            transform.SetParent(null);
            
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (bulletType.hitEffectPrefab)
            {
                var contactPoint = other.contacts[0];
                _hitEffect = Instantiate(bulletType.hitEffectPrefab, contactPoint.point, Quaternion.identity);
            }
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.up * 0.25f);
        }

        void AnimationIsDone()
        {
            
        }
        
    }
}