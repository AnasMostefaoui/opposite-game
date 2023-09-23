using UnityEngine;

namespace OppositeGame._project.Scripts.CameraScripts
{
    public class CameraModifier : MonoBehaviour
    {
        [Range(0, 5)]
        [SerializeField] public float speedMultiplier;
        [SerializeField] public float zoomMultiplier;
        [SerializeField] public bool shouldShake;
        
        
    }
}