using System;
using OppositeGame._project.Scripts.Inputs;
using OppositeGame._project.Scripts.Utilities;
using UnityEngine;

namespace OppositeGame._project.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    { 
        [SerializeField] private GameObject player;
        [Header("Camera settings")]
        [SerializeField] private Camera cameraObject;

        private Animator _animationController;
        private InputReader _inputReader;

        private void Start()
        {
            _inputReader = GetComponent<InputReader>();
            if (cameraObject == null)
            {
                cameraObject = Camera.main;
            }
            _animationController = GetComponent<Animator>();
            GameManager.Instance.OnMainMenu += OnMainMenu;
        }

        private void OnMainMenu(object sender, EventArgs e)
        {
            transform.position = transform.position.With(x: 0).With(y: 0);
        }

        private void Update()
        {
            var deltaTime = GameManager.Instance.IsPaused ? Time.deltaTime : Time.unscaledDeltaTime;
            

        }

        private void OnDestroy()
        {
            GameManager.Instance.OnMainMenu -= OnMainMenu;
        }
    }
}
