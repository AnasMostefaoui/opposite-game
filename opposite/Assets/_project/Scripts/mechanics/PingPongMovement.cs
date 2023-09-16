using UnityEngine;

namespace OppositeGame._project.Scripts.mechanics
{
    public class PingPongMovement : MonoBehaviour
    {
        [Header("Movement pattern settings")]
        // if these differs the spawner will move from start to end;
        [SerializeField] private Transform endPoint;
        
        private Vector3 _startPoint = Vector3.zero;
        private Vector3 _endPoint = Vector3.zero;
        
        private void Start()
        {
            _startPoint = transform.position;
            endPoint ??= transform;
            _endPoint = endPoint.position;
        }
        
        private void FixedUpdate()
        {
            var xDistance = _endPoint.x - _startPoint.x;
            var yDistance = _endPoint.y - _startPoint.y;
            var xPosition = xDistance > 0 ? _startPoint.x + Mathf.PingPong(Time.time, xDistance) : _startPoint.x;
            var yPosition = yDistance > 0 ? _startPoint.y + Mathf.PingPong(Time.time, yDistance) : _startPoint.y;
            var position = transform.position;
            var nextPosition =  new Vector3(xPosition, yPosition, position.z);
            transform.position = nextPosition;
        }

    }
}