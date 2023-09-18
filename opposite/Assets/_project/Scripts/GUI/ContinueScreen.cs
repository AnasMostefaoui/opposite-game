using System;
using UnityEngine;

namespace OppositeGame._project.Scripts.GUI
{
    public class ContinueScreen : MonoBehaviour
    {
        [SerializeField] private float continueTime = 13f;
        [SerializeField] private TMPro.TextMeshProUGUI continueTextMesh;
        [SerializeField] private TMPro.TextMeshProUGUI counterTextMesh;
        private float _continueTime;
        private bool _isContinueTimeOver;
        private void OnEnable()
        {
            counterTextMesh.text = $"{continueTime}";
            _continueTime = continueTime;
            // The continue screen in arcade don''t always respect the time
            // some will even slow when you get closer to 0 to give more chances to the player to insert a coin
            InvokeRepeating(nameof(UpdateCounter), 2f, 1.5f);
        }

        private void UpdateCounter()
        {
            _continueTime -= 1;
            counterTextMesh.text = $"{_continueTime}";
            if (_continueTime <= 0)
            {
                CancelInvoke(nameof(UpdateCounter));
                _continueTime = continueTime;
                _isContinueTimeOver = true;
            }
        }
        
        private void Update()
        {
            if (_isContinueTimeOver)
            {
                Debug.Log("Go to game over screen");
                gameObject.SetActive(false);
                // toggle game over in game manager event.
            }
        }
    }
}