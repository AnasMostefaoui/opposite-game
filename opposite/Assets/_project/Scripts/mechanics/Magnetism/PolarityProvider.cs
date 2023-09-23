using System;
using UnityEngine;

namespace OppositeGame._project.Scripts.mechanics.Magnetism
{
    public class PolarityProvider : MonoBehaviour
    {
        [SerializeField] private PolarityType polarityType = PolarityType.Blue;
        public delegate void PolarityChangeDelegate(PolarityType newPolarityType, PolarityType oldPolarityType);
        public event PolarityChangeDelegate OnPolarityChanged;

        public PolarityType PolarityType
        {
            get => polarityType;
            
            set
            {
                if (polarityType == value) return;
                var oldPolarity = polarityType;
                polarityType = value;
                // Raise the event to notify listeners about the change
                OnPolarityChanged?.Invoke(polarityType, oldPolarity);
            }
        }
    }
}