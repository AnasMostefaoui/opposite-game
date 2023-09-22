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

        private void Awake()
        {
            _lineRenderer = GetComponentInChildren<LineRenderer>();
            _boxCollider2D = GetComponentInChildren<BoxCollider2D>();
            Vector3[] positions = { startPoint.position, endPoint.position };
            var color = polarityType == PolarityType.Blue ? Color.blue : Color.red;
            _lineRenderer.startColor = color;
            _lineRenderer.endColor  = color;
            _lineRenderer.SetPositions(positions);
        }
    }
}