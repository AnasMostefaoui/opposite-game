using System;
using System.Collections;
using UnityEngine;

namespace OppositeGame._project.Scripts.GUI
{
    public class ContinueScreen : MonoBehaviour
    {
        [SerializeField] private float continueTime = 13f;
        [SerializeField] private TMPro.TextMeshProUGUI continueTextMesh;
        [SerializeField] private TMPro.TextMeshProUGUI counterTextMesh;
        
        public Action<GameScreen> OnLeaving;
        private float _continueTime;
        private bool _isContinueTimeOver;
        private bool _isTransitioningToGameOver;
        

        private void OnEnable()
        {
            GameManager.Instance.currentScreen = GameScreen.ContinueScreen;
            counterTextMesh.text = $"{continueTime}";
            _continueTime = continueTime;
            // The continue screen in arcade don''t always respect the time
            // some will even slow when you get closer to 0 to give more chances to the player to insert a coin
            InvokeRepeating(nameof(UpdateCounter), 2f, 1.5f);
        }

        private void OnDisable()
        {
            CancelInvoke(nameof(UpdateCounter));
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
            // we an also speed the sound when we get close to 0
        }

        private IEnumerator TransitionTo(GameScreen newScreen)
        {
            yield return new WaitForSeconds(2f);
            // maybe call this on menu manager? game manager?
            OnLeaving?.Invoke(newScreen);
        }
        private void Update()
        {
            if (_isContinueTimeOver && _isTransitioningToGameOver == false)
            {
                _isTransitioningToGameOver = true;
                // let the player see this screen with 0 time (maybe allow safe time)
                // then transition to game over
                StartCoroutine(TransitionTo(GameScreen.GameOver));
            }
        }
    }
}