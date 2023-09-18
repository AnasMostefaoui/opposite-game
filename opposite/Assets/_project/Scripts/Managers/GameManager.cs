using UnityEngine;

namespace OppositeGame._project.Scripts
{
    public enum GameScreen
    {
        MainMenu,
        Game,
        GameOver,
        Pause
    }
    
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private GameScreen currentScreen = GameScreen.MainMenu;
        public int Score { get; private set; }
        public int isGameOver { get; private set; }
        public int isPaused { get; private set; }
        public float timeScale { get; private set; }
        
        public event System.Action<int> OnScoreChanged;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            } 
            
        }
        
        public void AddScore(int score)
        {
            Score += score;
            OnScoreChanged?.Invoke(Score);
        }
        
    }
}