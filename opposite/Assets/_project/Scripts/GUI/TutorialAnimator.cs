using TMPro;
using UnityEngine;

namespace OppositeGame._project.Scripts.GUI
{
    public class TutorialAnimator : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI explanationText;
        private Animator _animator;
        
        public void OnPressToShootEvent()
        {
            explanationText.text = "PRESS SHOOT";
        }
        
        public void OnPressToSwapPolarityEvent()
        {
            explanationText.text = "SWAP POLARITY";
        }
        
        public void OnHoldToEnableShieldEvent()
        {
            explanationText.text = "HOLD FOR SHIELD";
        }
        
        public void OnHoldToPowerUpEvent()
        {
            explanationText.text = "S+D TIME SLOW";
        }

    }
}