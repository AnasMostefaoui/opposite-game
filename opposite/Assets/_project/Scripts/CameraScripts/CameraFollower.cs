using UnityEngine;

namespace OppositeGame._project.Scripts.CameraScripts
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform cameraObject;

        private void OnEnable()
        {
            cameraObject = Camera.main.transform;
        }

        private void LateUpdate()
        {
            transform.position = new Vector3(cameraObject.position.x, cameraObject.position.y, 0f);
        }
    }
}
