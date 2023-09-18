using System;
using OppositeGame._project.Scripts;
using UnityEngine;

namespace OppositeGame
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private  float initialSpeed = 0f;  // Initial speed (0 for stationary)
        [SerializeField] private  float maxSpeed = 2f;  // Target speed to reach
        [SerializeField] private  float accelerationRate = 0.2f;  // Rate of acceleration
        [SerializeField] private  float currentSpeed = 0;
        
        private bool _canMove = false;
        private void Awake()
        {
            GameManager.Instance.OnGameStarted += StartMoving;
            GameManager.Instance.OnGameOver += StopMoving;
            GameManager.Instance.OnContinueScreen += StopMoving;
            GameManager.Instance.OnContinuePlaying += StartMoving;
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
            if(_canMove == false) return;
            // make the camera slowly start moving.
            currentSpeed = Mathf.MoveTowards(currentSpeed, 
                maxSpeed, 
                // once at max speed don't accelerate any more.
                currentSpeed < maxSpeed ? accelerationRate * Time.deltaTime : 0);
            
            // we want the camera to move last.
            transform.position += Vector3.up * (currentSpeed * Time.deltaTime);
        }
    }
}