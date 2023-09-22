using System;
using UnityEngine;

namespace OppositeGame._project.Scripts.mechanics.Magnetism
{
    public class PolarityProvider : MonoBehaviour
    {
        [SerializeField] private PolarityType polarityType = PolarityType.Blue;
        private PolarityType _polarityType = PolarityType.Blue;
        public delegate void PolarityChangeDelegate(PolarityType newPolarityType, PolarityType oldPolarityType);
        public event PolarityChangeDelegate OnPolarityChanged;

        public PolarityType PolarityType
        {
            get => _polarityType;
            
            set
            {
                if (_polarityType == value) return;
                var oldPolarity = _polarityType;
                _polarityType = value;
                // Raise the event to notify listeners about the change
                OnPolarityChanged?.Invoke(_polarityType, oldPolarity);
            }
        }

        private void Update()
        {
            PolarityType = polarityType;
        }
    }
}