using System;
using OppositeGame._project.Scripts.Managers;
using OppositeGame._project.Scripts.Utilities;
using UnityEngine;

namespace OppositeGame._project.Scripts.CameraScripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private  float initialSpeed = 0f;  // Initial speed (0 for stationary)
        [SerializeField] private  float maxSpeed = 2f;  // Target speed to reach
        [SerializeField] private  float accelerationRate = 0.2f;  // Rate of acceleration
        [SerializeField] private  float currentSpeed = 0;
        [SerializeField] private  float zoomSpeed = 1;
        [SerializeField] private  float zoomFactor = 1;
        private Camera thisCamera;
        private float originalSize;
        private bool _canMove;
        private void Awake()
        {
            GameManager.Instance.OnGameStarted += StartMoving;
            GameManager.Instance.OnGameOver += StopMoving;
            GameManager.Instance.OnContinueScreen += StopMoving;
            GameManager.Instance.OnContinuePlaying += StartMoving;
            GameManager.Instance.OnMainMenu += ResetPosition;
            thisCamera = GetComponent<Camera>();
            originalSize = thisCamera.orthographicSize;
        }

        private void ResetPosition(object sender, EventArgs e)
        {
            transform.position = transform.position.With(x: 0).With(y: 0);
        }

        private void StartMoving(object sender, EventArgs e)
        {
            _canMove = true;
        }
        private void StopMoving(object sender, EventArgs e)
        {
            _canMove = false;
        }
        

        private void Start()
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }

            var playerPosition = player.position;
            transform.position = new Vector3(playerPosition.x, playerPosition.y, transform.position.z);
        }

        private void LateUpdate()
        {
            HandleZoom();
            if(_canMove == false) return;
            // make the camera slowly start moving.
            currentSpeed = Mathf.MoveTowards(currentSpeed, 
                maxSpeed, 
                // once at max speed don't accelerate any more.
                currentSpeed < maxSpeed ? accelerationRate * Time.deltaTime : 0);
            
            // we want the camera to move last.
            transform.position += Vector3.up * (currentSpeed * Time.deltaTime);
            
        }

        private void HandleZoom()
        {
            float targetSize = originalSize * zoomFactor;
            if (targetSize != thisCamera.orthographicSize)
            {
                thisCamera.orthographicSize = Mathf.Lerp(thisCamera.orthographicSize, 
                    targetSize, Time.deltaTime * zoomSpeed);
            }
            
            thisCamera.orthographicSize = Mathf.Lerp(thisCamera.orthographicSize, 
                targetSize, Time.deltaTime * zoomSpeed);
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnGameStarted -= StartMoving;
            GameManager.Instance.OnGameOver -= StopMoving;
            GameManager.Instance.OnContinueScreen -= StopMoving;
            GameManager.Instance.OnContinuePlaying -= StartMoving;
            GameManager.Instance.OnMainMenu -= ResetPosition;
        }
    }
}