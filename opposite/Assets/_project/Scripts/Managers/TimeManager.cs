using UnityEngine;

namespace OppositeGame._project.Scripts.Managers
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] public float slowTimeScale = 0.5f;
        [SerializeField] public float timeChangeSpeed = 10f;
        [SerializeField] public float slowTimeDuration = 0.5f;
        public bool IsTimePaused => Time.timeScale == 0f;   
        public bool IsTimeSlowed => _isSlowTimeDurationTimerActive;
        public float normalTimeScale = 1f;
        private float _slowTimeDurationTimer;
        private bool _isSlowTimeActive;
        private bool _isSlowTimeDurationTimerActive;
        private bool _forcePause;
        
        public void SlowTime()
        {
            _isSlowTimeActive = true;
            _isSlowTimeDurationTimerActive = true;
        }
        
        
        public void Update()
        {
            if(_forcePause) return;
            var targetedTimeScale = _isSlowTimeActive ? slowTimeScale : normalTimeScale;
            if (_isSlowTimeDurationTimerActive)
            {
                _slowTimeDurationTimer += Time.unscaledDeltaTime;
                if (_slowTimeDurationTimer >= slowTimeDuration)
                {
                    _isSlowTimeDurationTimerActive = false;
                    _slowTimeDurationTimer = 0f;
                    _isSlowTimeActive = false;
                }
            }
            Time.timeScale = Mathf.Lerp(Time.timeScale, targetedTimeScale, Time.unscaledDeltaTime * timeChangeSpeed);
        }
        
        public void PauseTime()
        {
            _forcePause = true;
            Time.timeScale = 0f;
        }
        
        public void ResumeTime()
        {
            _forcePause = false;
            Time.timeScale = normalTimeScale;
        }
    }
}