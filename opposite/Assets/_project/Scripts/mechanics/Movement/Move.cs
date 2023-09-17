using OppositeGame._project.Scripts.Utilities;
using UnityEngine;

namespace OppositeGame._project.Scripts.mechanics.Movement
{
    public class Move : MonoBehaviour
    {
        public Vector2 velocity =Vector2.zero;
        private Rigidbody2D _rb;
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.velocity = velocity; 
            transform.LookAt2D(velocity);
        }
        
        void FixedUpdate()
        {
            // update speed
           _rb.velocity = velocity;
            transform.LookAt2D(velocity);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, velocity.normalized * 0.25f);
        }
    }
}
