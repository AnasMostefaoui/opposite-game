namespace OppositeGame._project.Scripts
{
    public class GameManager
    {
        private static GameManager _instance;
        public static GameManager Instance => _instance ??= new GameManager();
        
        public int Score { get; private set; }
        public int isGameOver { get; private set; }
        public int isPaused { get; private set; }
        public float timeScale { get; private set; }
        
        public event System.Action<int> OnScoreChanged;
        
        public void AddScore(int score)
        {
            Score += score;
            OnScoreChanged?.Invoke(Score);
        }
        
    }
}