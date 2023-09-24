using System;
using OppositeGame._project.Scripts.mechanics.Magnetism;
using OppositeGame._project.Scripts.Patterns;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OppositeGame._project.Scripts.Managers
{
    public enum GameScreen
    {
        MainMenu,
        Game,
        ContinueScreen,
        GameOver,
        Pause
    }
    
    public class GameManager : MonoBehaviour
    {
        [SerializeField] public GameScreen currentScreen = GameScreen.MainMenu;
        public static GameManager Instance { get; private set; }
        public event EventHandler OnGameOver;
        public event EventHandler OnGamePaused;
        public event EventHandler OnGameResumed;
        public event EventHandler OnGameStarted;
        public event EventHandler OnContinueScreen;
        /**
         * This event is used to notify the ContinueScreen that the player has inserted a coin
         */
        public event EventHandler OnContinuePlaying;
        public event EventHandler OnMainMenu;
        public delegate void EnergyChanged(float value, PolarityType polarity);
        public delegate void DamageFeedback(float damageAmount, PolarityType polarity);
        public delegate void LifePointChanged(int lifePoint);
        public event EnergyChanged OnEnergyChanged;
        public event EnergyChanged OnDamageFeedbackRequested;
        public event LifePointChanged OnLifePointChanged;
        public bool IsGameStarted { get; set; }
        public bool hasLifePoints =>  currentLifePoint > 0;

        public bool IsGameOver { get; set; }
        public bool IsPaused => _timeManager.IsTimePaused;
        // normalized energy values
        public float RedCurrentEnergy = 1;
        public float BlueCurrentEnergy = 1;
        public int CurrentScore = 0;
        public int maxLifePoint = 2;

        public int currentLifePoint
        {
            get => _currentLifePoint;
            set
            {
                _currentLifePoint = value;
                OnLifePointChanged?.Invoke(value);
            }
        }
        public float totalPlayTime = 0;
        
        private int _currentLifePoint = 2;
        private TimeManager _timeManager;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
            }
            
            DontDestroyOnLoad(this);
            _timeManager = GetComponent<TimeManager>();
            _timeManager.normalTimeScale = Time.timeScale;
        }

        public bool IsRedShieldOn = false;
        public bool IsBlueShieldOn = false;
        public void UpdateRedEnergy(float value, PolarityType polarityType)
        {
            if(polarityType == PolarityType.Red)
                RedCurrentEnergy = value;
            else
                BlueCurrentEnergy = value;
            
            OnEnergyChanged?.Invoke(value, polarityType);
        }

        public void RequestDamageFeedBack(float damage)
        {
            OnDamageFeedbackRequested?.Invoke(damage, PolarityType.Red);
        }
        
        private void Start()
        {
            ObjectPoolManager.Cleanup();
        }

        public void SlowTime()
        {
            _timeManager.SlowTime();
        }
        
        private void Update()
        {
            totalPlayTime += Time.unscaledDeltaTime;
            switch (currentScreen)
            {
                case GameScreen.Game when hasLifePoints == false:
                    Debug.Log("hasLifePoints " + hasLifePoints);
                    ContinueRequest();
                    break;
                case GameScreen.ContinueScreen when IsGameOver:
                    GameIsOver();
                    break;
            }
        }
        
        private void ResetToMainMenu()
        {
            CurrentScore = 0;
            currentLifePoint = maxLifePoint;
            IsGameStarted = false;
            IsGameOver = false;
            currentScreen = GameScreen.MainMenu;
            _timeManager.ResumeTime();
            OnMainMenu?.Invoke(this, EventArgs.Empty);
        }

        public void GameIsOver()
        {
            _timeManager.PauseTime();
            currentScreen = GameScreen.GameOver;
            OnGameOver?.Invoke(this, EventArgs.Empty);
        }
        public void StartGame()
        {
            totalPlayTime = 0;
            IsGameStarted = true;
            currentScreen = GameScreen.Game;
            OnGameStarted?.Invoke(null, EventArgs.Empty);
        }

        public void ContinueRequest()
        {
            currentScreen = GameScreen.ContinueScreen;
            OnContinueScreen?.Invoke(this, EventArgs.Empty);
        }
        
        public void Revive()
        {
            currentLifePoint = maxLifePoint;
            IsGameOver = false;
            currentScreen = GameScreen.Game;
            OnContinuePlaying?.Invoke(null, EventArgs.Empty); 
        }
        
        public void Pause()
        {
            _timeManager.PauseTime();
            OnOnGamePaused();
        }
        
        public void Resume()
        {
            _timeManager.ResumeTime();
            OnGameResumed?.Invoke(null, EventArgs.Empty);
        }

        public void RestartTheLevel()
        {
        }
        
        public void RestartFromMainMenu()
        {
            ResetToMainMenu();
            ObjectPoolManager.Cleanup();
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
        
        private void OnOnGamePaused()
        {
            OnGamePaused?.Invoke(null, EventArgs.Empty);
        }

        #region Cleanup

        private void OnDestroy()
        {
            CleanupEventsHandlers();
        }
        
        private void CleanupEventsHandlers()
        {
            UnsubscribeFromEvents(OnGameOver);
            UnsubscribeFromEvents(OnGamePaused);
            UnsubscribeFromEvents(OnGameResumed);
            UnsubscribeFromEvents(OnGameStarted);
            UnsubscribeFromEvents(OnContinueScreen);
            UnsubscribeFromEvents(OnContinuePlaying);
        }
        
        private void UnsubscribeFromEvents(EventHandler eventHandler)
        {
            var subscribers = eventHandler?.GetInvocationList();
            if (subscribers != null)
            {
                foreach (var subscriber in subscribers)
                {
                    eventHandler -= subscriber as EventHandler;
                }
            }
        }


        #endregion
    }
}