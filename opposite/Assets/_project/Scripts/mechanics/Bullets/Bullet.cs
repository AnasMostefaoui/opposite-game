using OppositeGame._project.Scripts.mechanics.Movement;
using OppositeGame._project.Scripts.ScriptablesObjects;
using UnityEngine;
using UnityEngine.Pool;

namespace OppositeGame._project.Scripts.mechanics.Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private BulletType bulletType;
        private float _speed = 1f;
        private GameObject _firingEffect;
        private GameObject _bullet;
        

        public void SetBulletSpeed(float speed)
        {
            _speed = speed;
        }
        
        private void Start()
        {
            if (bulletType.firingEffectPrefab)
            {
                _firingEffect = Instantiate(bulletType.firingEffectPrefab, transform.position, Quaternion.identity);
                _firingEffect.transform.forward = gameObject.transform.forward;
                _firingEffect.transform.SetParent(transform);
            }
            
            Destroy(gameObject, bulletType.lifeTime);
        }

        private void Update()
        {
            transform.SetParent(null);
            transform.position += transform.up * (_speed * Time.deltaTime);
            
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (bulletType.hitEffectPrefab)
            {
                var contactPoint = other.contacts[0];
                Instantiate(bulletType.hitEffectPrefab, contactPoint.point, Quaternion.identity);
            }
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.up * 0.25f);
        }

    }

    public class BulletSpawner : MonoBehaviour
    {
        public ObjectPool<GameObject> bulletPool;
        public BulletType bulletType;
        
        private void Start()
        {
            bulletPool = new ObjectPool<GameObject>(CreateBullet,
                onBulletRequest, 
                onReturnBullet,
                OnDestroyBullet, 
                true, 
                200, 
                500);
        }

        private GameObject CreateBullet()
        {
            var bullet = Instantiate(bulletType.bulletPrefab, transform.position, transform.rotation);
            return bullet;
        }
        
        private void onBulletRequest(GameObject bullet)
        {
           // whhaht to once we got the bullet? rotate, move, etc
           
           bullet.SetActive(true);
        }
        
        private void onReturnBullet(GameObject bullet)
        {
            bullet.SetActive(false);
        }
        
        private void OnDestroyBullet(GameObject bullet)
        {
            Destroy(bullet);
        }
    }
}