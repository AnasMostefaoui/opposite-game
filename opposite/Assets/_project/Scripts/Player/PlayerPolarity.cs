using OppositeGame._project.Scripts.Inputs;
using OppositeGame._project.Scripts.mechanics.Magnetism;
using Unity.VisualScripting;
using UnityEngine;

namespace OppositeGame._project.Scripts.Player
{
    public class PlayerPolarity : PolarityProvider
    {
        private Animator _animationController;
        private InputReader _inputReader;
        private SpriteRenderer _spriteRenderer;
        private TrailRenderer _trailRenderer;
         
        private void Awake()
        {
            _inputReader = GetComponent<InputReader>();
            _animationController = GetComponent<Animator>();
            OnPolarityChanged += OnOnPolarityChanged;
        }

        private void OnOnPolarityChanged(PolarityType newPolarityType, PolarityType oldPolarityType)
        {
            _animationController.SetBool("isRed", PolarityType == PolarityType.Red);
            _animationController.SetBool("isBlue", PolarityType == PolarityType.Blue);
        }

        private void OnDestroy()
        {
            OnPolarityChanged -= OnOnPolarityChanged;
        }
    }
}