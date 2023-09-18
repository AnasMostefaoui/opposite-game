using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace OppositeGame._project.Scripts
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

        
        [SerializeField] public GameScreen currentScreen = GameScreen.MainMenu;
        public bool IsGameStarted { get; set; }
        
        public bool HasNoLifePoints { get; set; }
        public bool IsGameOver { get; set; }

        public bool IsPaused { get;   private set;  }

        private float TimeScale { get; set; }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            } 
            
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
        
        private void ResetManager()
        {
            IsGameOver = false;
            IsGameStarted = true;
            IsPaused = false;
            currentScreen = GameScreen.MainMenu;
        }

        private void PauseTime()
        {
            IsPaused = true;
            TimeScale = Time.timeScale;
            Time.timeScale = 0;
        }

        private void ResumeTime()
        {
            IsPaused = false;
            Time.timeScale = TimeScale;
            currentScreen = GameScreen.Game;
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
            currentScreen = GameScreen.Pause;
            OnOnGamePaused();
        }
        
        public void Resume()
        {
            ResumeTime();
            currentScreen = GameScreen.Game;
            OnGameResumed?.Invoke(null, EventArgs.Empty);
        }

        public void RestartTheLevel()
        {
        }
        
        public void RestartFromMainMenu()
        {
            currentScreen = GameScreen.MainMenu;   
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