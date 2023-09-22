using System;
using OppositeGame._project.Scripts.mechanics.Magnetism;
using Unity.VisualScripting;
using UnityEngine;

namespace OppositeGame._project.Scripts.mechanics.Traps
{
    public class LaserTrap : MonoBehaviour
    {
        [SerializeField] public float damage;
        [SerializeField] public PolarityType polarityType;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;
        
        private LineRenderer _lineRenderer;
        private BoxCollider2D _boxCollider2D;
        private Vector3[] _positions = new Vector3[2];
        private void Awake()
        {
            _lineRenderer = GetComponentInChildren<LineRenderer>();
            _boxCollider2D = GetComponentInChildren<BoxCollider2D>();
            _positions[0] = startPoint.position;
            _positions[1] = endPoint.position;
            
            var color = polarityType == PolarityType.Blue ? Color.blue : Color.red;
            _lineRenderer.startColor = color;
            _lineRenderer.endColor  = color;
            _lineRenderer.positionCount = _positions.Length;
            _lineRenderer.SetPositions(_positions);
            
            var collisionXSize = Vector3.Distance(startPoint.position, endPoint.position);
            _boxCollider2D.size = new Vector2(collisionXSize, _boxCollider2D.size.y);
        }

        private void Update()
        {
            _lineRenderer.SetPositions(_positions);
        }
    }
}