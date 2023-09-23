using System;
using UnityEngine;

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
    
    public enum GameLevels
    {
        Level1 = 0,
        Level2,
    }

    
    public class GameManager : MonoBehaviour
    {
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

        
        [SerializeField] public GameScreen currentScreen = GameScreen.MainMenu;
        public bool IsGameStarted { get; set; }
        
        public bool HasNoLifePoints { get; set; }
        public bool IsGameOver { get; set; }

        public bool IsPaused { get;   private set;  }

        private float TimeScale { get; set; }
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
            
            DontDestroyOnLoad(this);
            TimeScale = Time.timeScale;
            
        }

        private void Update()
        {
            switch (currentScreen)
            {
                case GameScreen.Game when HasNoLifePoints:
                    ContinueRequest();
                    break;
                case GameScreen.ContinueScreen when IsGameOver:
                    GameIsOver();
                    break;
            }
        }
        
        private void ResetToMainMenu()
        {
            HasNoLifePoints = false; // default life points
            IsGameStarted = false;
            IsGameOver = false;
            IsPaused = false;
            currentScreen = GameScreen.MainMenu;
            Time.timeScale = TimeScale;
            OnMainMenu?.Invoke(this, EventArgs.Empty);
        }

        private void PauseTime()
        {
            IsPaused = true;
            Time.timeScale = 0;
        }

        private void ResumeTime()
        {
            IsPaused = false;
            Time.timeScale = 1;
        }
        
        private void GameIsOver()
        {
            PauseTime(); 
            currentScreen = GameScreen.GameOver;
            OnGameOver?.Invoke(this, EventArgs.Empty);
        }
        public void StartGame()
        {
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
            IsGameOver = false;
            currentScreen = GameScreen.Game;
            HasNoLifePoints = false;
            OnContinuePlaying?.Invoke(null, EventArgs.Empty); 
        }
        
        public void Pause()
        {
            PauseTime(); 
            OnOnGamePaused();
        }
        
        public void Resume()
        {
            ResumeTime();
            OnGameResumed?.Invoke(null, EventArgs.Empty);
        }

        public void RestartTheLevel()
        {
        }
        
        public void RestartFromMainMenu()
        {
            ResetToMainMenu();
            // int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            // SceneManager.LoadScene(currentSceneIndex);
        }
        
        public void QuiteTheGame()
        {
            
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