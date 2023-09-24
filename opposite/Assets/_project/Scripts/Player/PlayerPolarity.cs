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
            OnPolarityChanged += OnInternalPolarityChanged;
            _inputReader.OnPolarityChanged += OnInputPolarityChanged;
            OnInternalPolarityChanged(PolarityType, PolarityType);
        }

        private void OnInputPolarityChanged(PolarityType newPolarity)
        {
            PolarityType = newPolarity;
        }

        private void OnInternalPolarityChanged(PolarityType newPolarityType, PolarityType oldPolarityType)
        {
            _animationController.SetBool("isRed", PolarityType == PolarityType.Red);
            _animationController.SetBool("isBlue", PolarityType == PolarityType.Blue);
        }

        private void OnDestroy()
        {
            OnPolarityChanged -= OnInternalPolarityChanged;
            _inputReader.OnPolarityChanged -= OnInputPolarityChanged;
        }
    }
}