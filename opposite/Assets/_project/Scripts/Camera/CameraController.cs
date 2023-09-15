using System;
using UnityEngine;

namespace OppositeGame
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float speed = 2f;

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
            // we want the camera to move last.
            transform.position += Vector3.up * (speed * Time.deltaTime);
        }
    }
}